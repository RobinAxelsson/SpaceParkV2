﻿using SpacePark_ModelsDB.Database;
using SpacePark_ModelsDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePark_ModelsDB.Database
{
    public class DbRepository : IStarwarsRepository
    {
        private readonly StarwarsContext _db;

        public DbRepository(StarwarsContext db)
        {
            _db = db;
        }

        public IQueryable<Person> Books => _db.Persons;
        public IQueryable<Account> Accounts => _db.Accounts;
        public IQueryable<SpaceShip> SpaceShips => _db.SpaceShips;
        public IQueryable<Receipt> Receipts => _db.Receipts;

        public void Add<EntityType>(EntityType entity) => _db.Add(entity);
        public void SaveChanges() => _db.SaveChanges();
    }
}