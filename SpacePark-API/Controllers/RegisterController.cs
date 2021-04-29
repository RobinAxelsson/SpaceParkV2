using Microsoft.AspNetCore.Mvc;
using SpacePark_ModelsDB.Database;
using SpacePark_ModelsDB.Models;
using SpacePark_ModelsDB.Networking;
using SpaceParkTests;
using System.Net;

namespace SpacePark_API.Controllers
{
    public class RegisterController : ControllerBase
    {
        private readonly IStarwarsRepository _repository;
        public RegisterController(IStarwarsRepository repository)
        {
            _repository = repository;
        }

        [Route("api/[controller]")]
        // GET: api/<UserController>
        [HttpPost]
        public string Post(string name)
        {
            var person = APICollector.ParseUser(name);
            if (person == null)
            {
                return HttpStatusCode.NotFound.ToString();
            }
            else
            {
                LocalTestDatabase.Persons.Add(person);
                return HttpStatusCode.OK.ToString();
            }
        }
    }
}