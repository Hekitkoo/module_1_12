using ORM.Core.Models;

namespace ORM.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<Warehouse, int> Warehouses { get; }
        public IRepository<Route, int> Routes { get; }
        public void RollBack();
        public void Commit();
    }
}