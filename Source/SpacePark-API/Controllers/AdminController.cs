using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using SpacePark_API.Authentication;
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
        [AuthorizeRoles(Role.Administrator)]
        [HttpGet]
        [Route("api/[controller]/Accounts")]
        public List<Account> Get()
        {
            var accounts = _repository.Accounts
                .Include(p => p.Person)
                .Include(ss => ss.SpaceShip)
                .Include(hw => hw.Person.Homeplanet).ToList();
            return accounts;
        }
        [AuthorizeRoles(Role.Administrator)]
        [HttpPost]
        [Route("api/[controller]/AddSpacePort")]
        public IActionResult Get([FromBody] AddSpacePortModel model)
        {
            if (_repository.SpacePorts.Any() && _repository.SpacePorts.Single(p => p.Name == model.Name) != null)
                return Forbid();


            var port = new SpacePort(model.Name, model.ParkingSpots, model.PriceMultiplier) {Enabled = true };
                                                                                            //Mark the port as enabled so we can park.
            _repository.Add(port);
            _repository.SaveChanges();

            return Ok(new {
                Name = port.Name, 
                ParkingSpots = model.ParkingSpots,
                PriceMultiplier = model.PriceMultiplier,
                
            });
        }
        [AuthorizeRoles(Role.Administrator)]
        [HttpPost]
        [Route("api/[controller]/SpacePortEnabled")]
        public IActionResult Get([FromBody] ChangeSpacePortAvailability model)
        {
            if (!_repository.SpacePorts.Any() && _repository.SpacePorts.Single(p => p.Name == model.Name) == null)
                return NotFound();
            var port = _repository.SpacePorts.Single(p => p.Name == model.Name);
            port.Enabled = false;
            _repository.Update(port);
            _repository.SaveChanges();
            return Ok(
                $"SpacePort with the name: {model.Name} has been set to {model.Enabled}"
            );
        }
        [AuthorizeRoles(Role.Administrator)]
        [HttpPost]
        [Route("api/[controller]/DeleteSpacePort")]
        public IActionResult Get([FromBody] RemoveSpacePortModel model)
        {
            if (!_repository.SpacePorts.Any() && _repository.SpacePorts.Single(p => p.Name == model.Name) == null)
                return NotFound();
            var port = _repository.SpacePorts.Single(p => p.Name == model.Name);
            _repository.Remove(port);
            _repository.SaveChanges();
            return Ok(
                $"SpacePort with the name: {model.Name} has been removed"
            );
        }
        [AuthorizeRoles(Role.Administrator)]
        [HttpPost]
        [Route("api/[controller]/UpdateSpacePortPrice")]
        public IActionResult Get([FromBody] UpdateSpacePortPrice model)
        {
            if (!_repository.SpacePorts.Any() && _repository.SpacePorts.Single(p => p.Name == model.Name) == null)
                return NotFound();
            var port = _repository.SpacePorts.Single(p => p.Name == model.Name);
            var oldMultiplier = port.PriceMultiplier; //Cache old mult. for message
            port.PriceMultiplier = model.SpacePortMultiplier;
            _repository.Update(port);
            _repository.SaveChanges();
            return Ok(
                $"SpacePort with the name: {model.Name} has been updated with the new price multiplier to {model.SpacePortMultiplier} from {oldMultiplier}"
            );
        }
        [AuthorizeRoles(Role.Administrator)]
        [HttpPost]
        [Route("api/[controller]/UpdateSpacePortName")]
        public IActionResult Get([FromBody] UpdateSpacePortName model)
        {
            if (!_repository.SpacePorts.Any())
                return NotFound("There are no registered Spaceports.");
            if (_repository.SpacePorts.Single(p => p.Name == model.NewSpacePortName) != null)
                return Forbid("Name is already taken");
            var port = _repository.SpacePorts.Single(p => p.Name == model.OldSpacePortName);
            port.Name = model.NewSpacePortName;
            _repository.Update(port);
            _repository.SaveChanges();
            return Ok(
                $"SpacePort with the previous name of {model.OldSpacePortName} has gained a new name; {model.NewSpacePortName}"
            );
        }
        [AuthorizeRoles(Role.Administrator)]
        [HttpPost]
        [Route("api/[controller]/PromoteAdmin")]
        public IActionResult Get([FromBody] PromoteAdminModel model)
        {
            if (!_repository.Accounts.Any())
                return NotFound("There are no registered accounts.");
            if (_repository.Accounts.Single(a => a.AccountName == model.AccountName) == null)
                return NotFound();
            
            var account = _repository.Accounts.Single(a => a.AccountName == model.AccountName);
            account.Role = Role.Administrator;
            _repository.Update(account);
            _repository.SaveChanges();
            return Ok(
                $"Account with the name of {model.AccountName} has been promoted to administrator"
            );
        }
        [AuthorizeRoles(Role.Administrator)]
        [HttpPost]
        [Route("api/[controller]/PromoteAdmin")]
        public IActionResult Get([FromBody] DemoteAdminModel model)
        {
            if (!_repository.Accounts.Any())
                return NotFound("There are no registered accounts.");
            if (_repository.Accounts.Single(a => a.AccountName == model.AccountName) == null)
                return NotFound();
            
            var account = _repository.Accounts.Single(a => a.AccountName == model.AccountName);
            account.Role = Role.User;
            _repository.Update(account);
            _repository.SaveChanges();
            return Ok(
                $"Account with the name of {model.AccountName} has been demoted to user"
            );
        }
        [AuthorizeRoles(Role.Administrator)]
        [HttpGet]
        [Route("api/[controller]/GetAccount")]
        public List<Account> Get(string name)
        {
            var accounts = _repository.Accounts
                .Where(a => a.AccountName == name)
                .Include(p => p.Person)
                .Include(ss => ss.SpaceShip)
                .Include(hw => hw.Person.Homeplanet).ToList();
            return accounts;
        }
    }
}