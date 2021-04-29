using SpacePark_ModelsDB.Models;
using System.Linq;

namespace SpacePark_ModelsDB.Database
{
    public interface IStarwarsRepository
    {
        IQueryable<Account> Accounts { get; }
        IQueryable<Person> People { get; }
        IQueryable<Receipt> Receipts { get; }
        IQueryable<SpaceShip> SpaceShips { get; }
        IQueryable<Homeworld> Homeworlds { get; }
        void Add<EntityType>(EntityType entity);
        void SaveChanges();
    }
}