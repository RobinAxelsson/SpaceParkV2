using Microsoft.AspNetCore.Mvc;
using SpacePark_API.Networking;
using SpaceParkTests;

namespace SpacePark_API.Controllers
{
    public class PricingController : ControllerBase
    {
        [Route("api/[controller]")]
        [HttpGet]
        public double Get(string SpaceShipModel, int hours)
        {
            var ship = APICollector.ParseShip(SpaceShipModel);
            
            var price = double.Parse(ship.ShipLength.Replace(".", ",")) * (hours * 60) / LocalTestDatabase._priceMultiplier;
            return price;
        }
    }
}