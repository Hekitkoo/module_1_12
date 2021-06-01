using System;
using System.Data.SqlClient;
using ORM.Core.Interfaces;
using ORM.Core.Models;
using ORM.Dapper.Repositories;

namespace ORM.Dapper
{
    public class DapperUnitOfWork : IUnitOfWork, IDisposable
    {
        public IRepository<Warehouse, int> Warehouses { get; private set; }
        public IRepository<Route, int> Routes { get; private set; }

        private SqlConnection Context { get; }

        public DapperUnitOfWork(string connectionString)
        {
            Routes = new DapperRouteRepository(connectionString);
        }

        public void RollBack()
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.transactions.transactionscope?redirectedfrom=MSDN&view=net-5.0
        }

        public void Commit()
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.transactions.transactionscope?redirectedfrom=MSDN&view=net-5.0
        }

        public void Dispose()
        {
            if (Context == null)
            {
                return;
            }
            Context.Close();
            Context.Dispose();

            Routes = null;
        }
    }
}