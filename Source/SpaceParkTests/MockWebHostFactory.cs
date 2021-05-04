using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SpacePark_API;
using SpacePark_API.DataAccess;
using System;
using System.Linq;
using System.Text;

namespace SpaceParkTests
{
    public class MockWebHostFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        public readonly string DbName = "MockDB";
        public IConfiguration Configuration { get; private set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<StarwarsContext>));

                services.Remove(descriptor);

                services.AddDbContext<StarwarsContext>(options =>
                {
                    options.UseInMemoryDatabase(databaseName: DbName);
                });
                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<StarwarsContext>();
                    
                    var logger = scopedServices
                        .GetRequiredService<ILogger<MockWebHostFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        //Utilities.InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }

    }
}
