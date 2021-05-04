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
    public class TestHost<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        public readonly string DbName = "MockDB";
        public IConfiguration Configuration { get; private set; }
        public StarwarsContext DbContext { get; private set; }
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
                    var dbContext = scopedServices.GetRequiredService<StarwarsContext>();
                    DbContext = dbContext;
                    var logger = scopedServices
                        .GetRequiredService<ILogger<TestHost<TStartup>>>();

                    dbContext.Database.EnsureCreated();
                }
            });
        }
        public void EnsureDbDeleted() => DbContext.Database.EnsureDeleted();
    }
}
