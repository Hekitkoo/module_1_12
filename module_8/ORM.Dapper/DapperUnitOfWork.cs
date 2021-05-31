using System;
using System.Data.SqlClient;
using ORM.Core.Interfaces;
using ORM.Core.Models;
using ORM.Dapper.Repositories;

namespace ORM.Dapper
{
    public class DapperUnitOfWork : IUnitOfWork, IDisposable
    {
        public IRepository<Route, int> Routes { get; private set; }

        private SqlConnection Context { get; }

        public DapperUnitOfWork(string connectionString)
        {
            Routes = new DapperRouteRepository(connectionString);
        }

        public void RollBack()
        {
            //As mentioned in https://docs.microsoft.com/en-us/dotnet/api/system.transactions.transactionscope?redirectedfrom=MSDN&view=net-5.0
            //we have transaction scope which in exception scope auto rollback it
        }

        public void Commit()
        {
            //As mentioned in https://docs.microsoft.com/en-us/dotnet/api/system.transactions.transactionscope?redirectedfrom=MSDN&view=net-5.0
            //we have transaction scope which at the end of scope auto Commit changes
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