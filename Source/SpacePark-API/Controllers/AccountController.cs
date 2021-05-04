using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SpacePark_API.Authentication;
using SpacePark_API.DataAccess;
using SpacePark_API.Models;
using SpacePark_API.Networking;

namespace SpacePark_API.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IStarwarsRepository _repository;
        private readonly IConfiguration _configuration;
        public AccountController(IStarwarsRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        // GET: api/<UserController>
        [HttpPost]
        [Route("api/[controller]/Register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            var person = APICollector.ParseUser(model.Name);
            //var ship = APICollector.ParseShip(model.SpaceShipModel);
            //if (person == null || ship == null)
            if (person == null)
            {
                return NotFound();
            }
            if (_repository.People.FirstOrDefault(p => p.Name == person.Name) != null)
            {
                return Forbid();
            }
            //ship = _repository.SpaceShips.FirstOrDefault(g => g.Model == ship.Model) ??
                   //ship;
            var account = new Account
            {
                AccountName = model.AccountName,
                Password = model.Password, //TODO add password hashing
                Person = person,
                //SpaceShip = ship
            };
            person.Homeplanet = _repository.Homeworlds.FirstOrDefault(g => g.Name == person.Homeplanet.Name) ??
                                person.Homeplanet;
            _repository.Add(account);
            _repository.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
        [HttpPost]
        [Route("api/[controller]/login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var account = _repository.Accounts.FirstOrDefault(x => x.AccountName == model.Username);
            if (account == null) return Unauthorized();

            if (account.Password == model.Password && account.AccountName == model.Username)
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
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