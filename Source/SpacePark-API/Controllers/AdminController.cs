using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SpacePark_API.DataAccess;
using SpacePark_API.Models;

namespace SpacePark_API.Controllers
{
    public class AdminController : ControllerBase
    {
        private readonly IStarwarsRepository _repository;
        public AdminController(IStarwarsRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<UserController>
        [HttpGet]
        [Route("api/[controller]/AddSpacePort")]
        public string Get(string Name, int ParkingSpots, double PriceMultiplier)
        {
            var port = new SpacePort(Name, ParkingSpots, PriceMultiplier);
            if (_repository.SpacePorts.Single(p => p.Name == Name) == null)
            {
                _repository.Add(port);
                _repository.SaveChanges();
                return HttpStatusCode.OK.ToString();
            }
            else
            {
                return HttpStatusCode.Forbidden.ToString();
            }
         
        }
    }
}