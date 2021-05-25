using System;
using ORM.Core.Interfaces;
using ORM.Core.Models;
using ORM.EF.Repositories;

namespace ORM.EF
{
    public class EfUnitOfWork : IUnitOfWork, IDisposable
    {
        public IRepository<Warehouse, int> Warehouses { get; }
        public IRepository<Route, int> Routes { get; private set; }

        private EFContext context;

        public EfUnitOfWork(string connectionString)
        {
            context = new EFContext(connectionString);
            Routes = new EFRouteRepository(context);
        }

        public void RollBack()
        {
            // https://docs.microsoft.com/en-us/ef/core/saving/transactions
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            if (context == null)
            {
                return;
            }
            context.Dispose();
            context = null;
            Routes = null;
        }
    }
}