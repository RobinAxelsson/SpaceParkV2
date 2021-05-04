using SpacePark_API.Models;
using System.Linq;

namespace SpacePark_API.DataAccess
{
    public interface IStarwarsRepository
    {
        IQueryable<Account> Accounts { get; }
        IQueryable<Person> People { get; }
        IQueryable<Receipt> Receipts { get; }
        IQueryable<SpaceShip> SpaceShips { get; }
        IQueryable<Homeworld> Homeworlds { get; }
        IQueryable<SpacePort> SpacePorts { get; }
        IQueryable<UserToken> UserTokens { get; }
        void Add<EntityType>(EntityType entity);
        void Update<EntityType>(EntityType entity);
        void SaveChanges();
    }
}