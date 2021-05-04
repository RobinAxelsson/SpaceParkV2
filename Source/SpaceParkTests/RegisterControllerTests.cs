using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using SpacePark_API;
using SpacePark_API.Controllers;
using SpacePark_API.DataAccess;
using SpacePark_API.Models;
using Microsoft.Extensions.Configuration;

namespace SpaceParkTests
{
    public class RegisterControllerTests : IClassFixture<TestHost<Startup>>
    {
        private AccountController _controller;
        private IStarwarsRepository _repository;
        public HttpClient Client { get; }
        public RegisterControllerTests(TestHost<Startup> factory)
        {
            Client = factory.CreateClient();
            _repository = GetInMemoryRepository(factory.DbName);
            _controller = new AccountController(_repository, factory.Configuration);
        }
        private void Populate(StarwarsContext context)
        {
            var xWing = new SpaceShip { Model = "X-Wing" };
            var luke = new Person() { Name = "Luke Skywalker" };
            var lsAccount = new Account() { SpaceShip = xWing, Person = luke };

            context.Add(lsAccount);

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
        //[Fact]
        //public async Task IdentifyAgainstSwapi_ExpectOK()
        //{
        //    var response = await Client.GetAsync("/api/register?name=yoda");
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //}
        //[Fact]
        //public async Task IdentifyAgainstSwapi_Expect404()
        //{
        //    var response = await Client.GetAsync("/api/register?name=yodas");
        //    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        //}
    }
}
