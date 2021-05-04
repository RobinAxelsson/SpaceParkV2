using System.Linq;
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
using SpacePark_API.Networking;
using Microsoft.Extensions.Configuration;

namespace SpaceParkTests
{
    public class AccountControllerTests : IClassFixture<MockWebHostFactory<Startup>>
    {
        private AccountController _controller;
        private IStarwarsRepository _repository;
        public HttpClient Client { get; }
        public AccountControllerTests(MockWebHostFactory<Startup> factory, IConfiguration configuration)
        {
            Client = factory.CreateClient();
            _repository = GetInMemoryRepository(factory.DbName);
            _controller = new AccountController(_repository, configuration);
        }
        private void Populate(StarwarsContext context)
        {
            var xWing = new SpaceShip { Model = "X-Wing" };
            var luke = new Person() { Name = "Luke Skywalker" };
            var lsAccount = new Account() { SpaceShip = xWing, Person = luke, AccountName = "LuckyLuke" };

            context.Add(lsAccount);
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

            var model = _repository.People.Single(p => p.Name == "Luke Skywalker");
            Assert.Equal("Luke Skywalker", model.Name);
        }
        [Fact]
        public void GetAccountFromDb_expectLuke()
        {
            var model = _repository.Accounts.Single(p => p.AccountName == "LuckyLuke");
            Assert.Equal("LuckyLuke", model.AccountName);
        }
    }
}
