using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SpacePark_API.Authentication
{
    //https://www.c-sharpcorner.com/article/authentication-and-authorization-in-asp-net-core-web-api-with-json-web-tokens/
    //https://stackoverflow.com/questions/43767933/entity-framework-core-using-multiple-dbcontexts
    //use in package manager console:
    //      "add-migration init -context AuthDbContext"
    //      "update-database -context AuthDbContext"
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
