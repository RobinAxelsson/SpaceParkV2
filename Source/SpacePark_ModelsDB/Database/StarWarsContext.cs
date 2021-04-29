using Microsoft.EntityFrameworkCore;
using SpacePark_ModelsDB.Models;

namespace SpacePark_ModelsDB.Database
{
    public class StarwarsContext : DbContext
    {
        public string ConnectionString;
        public DbSet<SpaceShip> SpaceShips { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Person.Homeworld> Homeworlds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            //hard code connectionstring in onconfiguring method when migrating
            ConnectionString = @"Server = 90.229.161.68,52578; Database = StarwarsProjectLive; User Id = adminuser; Password = starwars;";
            optionsbuilder.UseSqlServer(ConnectionString);
        }
    }
}