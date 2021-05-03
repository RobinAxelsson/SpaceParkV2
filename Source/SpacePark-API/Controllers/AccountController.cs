using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SpacePark_API.DataAccess;
using SpacePark_API.Models;
using SpacePark_API.Networking;

namespace SpacePark_API.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IStarwarsRepository _repository;
        public AccountController(IStarwarsRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<UserController>
        [HttpGet]
        [Route("api/[controller]/Register")]
        public string Get(string characterName, string spaceShipModel, string accountName)
        {
            var person = APICollector.ParseUser(characterName);
            var ship = APICollector.ParseShip(spaceShipModel);
            if (person == null || ship == null)
            {
                return HttpStatusCode.NotFound.ToString();
            }
            if (_repository.People.Single(p => p.Name == person.Name) == null)
            {
                return HttpStatusCode.Forbidden.ToString();
            }
            ship = _repository.SpaceShips.FirstOrDefault(g => g.Model == ship.Model) ??
                   ship;
            var account = new Account
            {
                AccountName = accountName,
                Person = person,
                SpaceShip = ship
            };
            person.Homeplanet = _repository.Homeworlds.FirstOrDefault(g => g.Name == person.Homeplanet.Name) ??
                                person.Homeplanet;
            _repository.Add(account);
            _repository.SaveChanges();
            return HttpStatusCode.OK.ToString();
        }
        [HttpGet]
        [Route("api/[controller]/Ships")]
        public List<SpaceShip> Get()
        {
            var ships = APICollector.ReturnShips().Where(s => double.Parse(s.ShipLength.Replace(".",",")) <= 150).ToList();
            return ships;
        }
    }
}