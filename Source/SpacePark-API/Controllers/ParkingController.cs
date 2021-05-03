using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpacePark_API.DataAccess;
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
        [HttpGet]
        [Route("api/[controller]/Park")]
        public string Get(int SpacePortID, int accountID, double minutes)
        {
            var spacePort = _repository.SpacePorts.Single(sp => sp.Id == SpacePortID);
            
            var account = _repository.Accounts.Where(a => a.AccountID == accountID)
                .Include(ss => ss.SpaceShip).Include(p => p.Person).Single();
            
            if (spacePort == null || account == null)
                return HttpStatusCode.NotFound.ToString();
            return spacePort.Park(account, minutes, _repository);
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