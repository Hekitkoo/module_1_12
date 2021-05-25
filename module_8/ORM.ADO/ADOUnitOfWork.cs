using System;
using System.Data.SqlClient;
using ORM.ADO.Repositories.Connected;
using ORM.ADO.Repositories.Disconnected;
using ORM.Core.Interfaces;
using ORM.Core.Models;

namespace ORM.ADO
{
    public class ADOUnitOfWork : IUnitOfWork, IDisposable
    {
        public IRepository<Warehouse, int> Warehouses { get; private set; }
        public IRepository<Route, int> Routes { get; private set; }

        public IRepository<Route, int> DisconnectedRoutes { get; private set; }
        private SqlConnection Context { get; }
        private SqlTransaction Transaction { get; }
        
        public ADOUnitOfWork(string connectionString)
        {
            Context = new SqlConnection(connectionString);
            Context.Open();
            Transaction = Context.BeginTransaction();
            Warehouses = new WarehouseRepository(Transaction, Context);
            Routes = new RouteRepository(Transaction, Context);

            DisconnectedRoutes = new DisconnectedRouteRepository(Transaction, Context);
        }

        public void RollBack()
        {
            Transaction.Rollback();
        }

        public void Commit()
        {
           Transaction.Commit();
        }

        public void Dispose()
        {
            Transaction?.Dispose();

            if (Context == null)
            {
                return;
            }
            Context.Close();
            Context.Dispose();

            Warehouses = null;
            Routes = null;
        }
    }
}