using SpacePark_API.DataAccess;
using SpacePark_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePark_API.DataAccess
{
    public class DbRepository : IStarwarsRepository
    {
        private readonly StarwarsContext _db;

        public DbRepository(StarwarsContext db)
        {
            _db = db;
        }

        public IQueryable<Person> People => _db.People;
        public IQueryable<Account> Accounts => _db.Accounts;
        public IQueryable<SpaceShip> SpaceShips => _db.SpaceShips;
        public IQueryable<Receipt> Receipts => _db.Receipts;
        public IQueryable<Homeworld> Homeworlds => _db.Homeworlds;
        public IQueryable<SpacePort> SpacePorts => _db.SpacePorts;

        public void Add<EntityType>(EntityType entity) => _db.Add(entity);
        public void SaveChanges() => _db.SaveChanges();
        public void Update<EntityType>(EntityType entity) => _db.Update(entity);
    }
}
