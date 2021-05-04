using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpacePark_API.Authentication;
using SpacePark_API.DataAccess;
using SpacePark_API.Models;
using SpacePark_API.Networking;

namespace SpacePark_API.Controllers
{
    
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IStarwarsRepository _repository;

        public ParkingController(IStarwarsRepository repository)
        {
            _repository = repository;
        }
        [Authorize]
        [HttpPost]
        [Route("api/[controller]/Park")]
        public IActionResult Post([FromBody] ParkingModelId model)
        {
            var spacePort = _repository.SpacePorts.Single(sp => sp.Id == model.SpacePortId);
            var token = Request.Headers.FirstOrDefault(p => p.Key == "Authorization").Value.FirstOrDefault()?.Replace("Bearer ", "");
            var account = _repository.UserTokens.Where(a => a.Token == token)
                .Include(a => a.Account).Include(p => p.Account.Person).Include(ss => ss.Account.SpaceShip).Single()
                .Account;
            if (account == null || spacePort == null)
                return NotFound();
            var price = spacePort.CalculatePrice(account.SpaceShip, model.Minutes);
            var receipt = new Receipt
            {
                Price = price,
                StartTime = DateTime.Now.ToString("d"),
                EndTime = DateTime.Now.AddMinutes(model.Minutes).ToString("d"),
                SpacePort = spacePort,
                Account = account
            };
            _repository.Add(receipt);
            _repository.SaveChanges();

            return Ok(new { 
                StartTime = receipt.StartTime,
                EndTime = receipt.EndTime,
                Price = receipt.Price,
                SpacePort = receipt.SpacePort.Name,
                PurchasedBy = account.Person.Name
            });
        }
        [HttpGet]
        [Route("api/[controller]/Price")]
        public decimal Get(int SpacePortID, string spaceShipModel, double minutes)
        {
            var spacePort = _repository.SpacePorts.Single(sp => sp.Id == SpacePortID);
            var spaceShip = APICollector.ParseShip(spaceShipModel);
            return spacePort.CalculatePrice(spaceShip, minutes);
        }
    }
}