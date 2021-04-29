using Microsoft.EntityFrameworkCore;
using SpacePark_ModelsDB.Models;

namespace SpacePark_ModelsDB.Database
{
    public class StarwarsContext : DbContext
    {
        public StarwarsContext(DbContextOptions<StarwarsContext> options) : base(options) { }
        public DbSet<SpaceShip> SpaceShips { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Homeworld> Homeworlds { get; set; }
    }
}