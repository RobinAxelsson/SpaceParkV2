using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpacePark_ModelsDB.Models;
using SpacePark_ModelsDB.Networking;
using SpaceParkTests;
using SpacePark_ModelsDB.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpacePark_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IStarwarsRepository _repository;

        public UserController(IStarwarsRepository repository)
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
