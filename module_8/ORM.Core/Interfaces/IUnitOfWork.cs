using ORM.Core.Models;

namespace ORM.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<Route, int> Routes { get; }
        public void RollBack();
        public void Commit();
    }
}