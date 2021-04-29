using SpacePark_ModelsDB.Database;
using SpacePark_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using SpacePark_API.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace SpaceParkTests
{
    public class PersonControllerTests : IClassFixture<WebApplicationFactory<SpacePark_API.Startup>>
    {
        private PersonController _controller;
        private IStarwarsRepository _repository;
        public HttpClient Client { get; }
        public PersonControllerTests(WebApplicationFactory<SpacePark_API.Startup> fixture)
        {

            _repository = GetInMemoryRepository();
            _controller = new PersonController(_repository);
            Client = fixture.CreateClient();
        }
        private void Populate(StarwarsContext context)
        {
            var xWing = new SpaceShip { Model = "X-Wing" };
            var luke = new Person() { Name = "Luke Skywalker" };
            var lsAccount = new Account() { SpaceShip = xWing, Person = luke };

            context.Add(lsAccount);

            context.SaveChanges();
        }
        private IStarwarsRepository GetInMemoryRepository()
        {
            var options = new DbContextOptionsBuilder<StarwarsContext>()
                             .UseInMemoryDatabase(databaseName: "MockDB")
                             .Options;

            var initContext = new StarwarsContext(options);
            initContext.Database.EnsureDeleted();

            Populate(initContext);
            var testContext = new StarwarsContext(options);
            var repository = new DbRepository(testContext);

            return repository;
        }
        [Fact]
        public void GetPersonFromDb_expectLuke() //from database
        {
            var result = _controller.Get(1);
            var model = result as Person;

            Assert.Equal("Luke Skywalker", model.Name);
        }
        [Fact]
        public async Task GetPersonFromAPI_ExpectOK() //from application api
        {
            var response = await Client.GetAsync("/api/person/1/");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task GetFromAPI_ExpectPerson() //from application api
        {
            var response = await Client.GetAsync("/api/person/1/");

            var person = JsonConvert.DeserializeObject<Person>(await response.Content.ReadAsStringAsync());
            Assert.Equal("Luke Skywalker", person.Name);
        }
    }
}
