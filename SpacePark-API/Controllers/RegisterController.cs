using Microsoft.AspNetCore.Mvc;
using SpacePark_ModelsDB.Database;
using SpacePark_ModelsDB.Models;
using SpacePark_ModelsDB.Networking;
using SpaceParkTests;

namespace SpacePark_API.Controllers
{
    public class RegisterController : ControllerBase
    {
        [Route("api/[controller]")]
        // GET: api/<UserController>
        [HttpPost]
        public string Post(string name)
        {
            var person = APICollector.ParseUser(name);
            if (person == null)
            {
                return "404 - Could not find user";
            }
            else
            {
                LocalTestDatabase.Persons.Add(person);
                return "200 - Success";
            }
            
        }
    }
}