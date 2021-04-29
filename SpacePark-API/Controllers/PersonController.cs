using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpacePark_API.Models;
using SpacePark_ModelsDB.Networking;
using SpaceParkTests;
using SpacePark_ModelsDB.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpacePark_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IStarwarsRepository _repository;

        public PersonController(IStarwarsRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<UserController>
        [HttpGet]
        public List<Person> Get()
        {
            //var list = LocalTestDatabase.Persons;
            var list = _repository.People;
            return list.ToList();
        }
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return _repository.People.Single(p => p.Id == id);
        }
    }
}
