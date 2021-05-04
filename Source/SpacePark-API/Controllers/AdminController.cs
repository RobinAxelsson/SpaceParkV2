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
        public class AddSpacePortModel
        {
            public string Name { get; set; }
            public int ParkingSpots { get; set; }
            public int PriceMultiplier { get; set; }
        }

        [HttpPost]
        [Route("api/[controller]/AddSpacePort")]
        public IActionResult Get([FromBody] AddSpacePortModel model)
        {
            
            if (_repository.SpacePorts.Count() == 0 || _repository.SpacePorts.Single(p => p.Name == model.Name) == null)
            {
                var port = new SpacePort(model.Name, model.ParkingSpots, model.PriceMultiplier);
                _repository.Add(port);
                _repository.SaveChanges();

                return Ok(new {
                    Name = port.Name, 
                    ParkingSpots = model.ParkingSpots,
                    PriceMultiplier = model.PriceMultiplier
                });
            }

            else
                return Forbid();
        }

    }
}