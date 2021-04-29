using SpacePark_API.DataAccess;
using SpacePark_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using SpacePark_API.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using SpacePark_API;

namespace SpaceParkTests
{
    public class PersonControllerTests : IClassFixture<MockWebHostFactory<Startup>>
    {
        private PersonController _controller;
        private IStarwarsRepository _repository;
        public HttpClient Client { get; }
        public PersonControllerTests(MockWebHostFactory<Startup> factory)
        {
            Client = factory.CreateClient();
            _repository = GetInMemoryRepository(factory.DbName);
            _controller = new PersonController(_repository);
        }
        private void Populate(StarwarsContext context)
        {
            var xWing = new SpaceShip { Model = "X-Wing" };
            var luke = new Person() { Name = "Luke Skywalker" };
            var lsAccount = new Account() { SpaceShip = xWing, Person = luke };

            context.Add(luke);
            //context.Add(lsAccount);

            context.SaveChanges();
        }
        private IStarwarsRepository GetInMemoryRepository(string dbName)
        {
            var options = new DbContextOptionsBuilder<StarwarsContext>()
                             .UseInMemoryDatabase(databaseName: dbName)
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
            var content = await response.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(content);
            Assert.Equal("Luke Skywalker", person.Name);
        }
    }
}
