using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ORM.Core.Interfaces;
using ORM.Core.Models;
using Dapper;

namespace ORM.Dapper.Repositories
{
    public class DapperRouteRepository : IRepository<Route, int>
    {
        private SqlConnection connection;

        public DapperRouteRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Create(Route entity)
        {
            using IDbConnection c = connection;
            const string createCommand = @"INSERT INTO [dbo].[Route](OriginWarehouseId, DestinationWarehouseId, Distance) 
            OUTPUT INSERTED.RouteId values (@originWarehouseId, @destinationWarehouseId, @distance)";
            c.Execute(createCommand, entity);
        }

        public Route GetById(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Route> GetAll()
        {
            List<Route> results;

            using IDbConnection db = connection;
            const string selectCommand = "SELECT * FROM  [dbo].[Route] WITH(NOLOCK)";
            results = db.Query<Route>(selectCommand).ToList();

            return results;
        }

        public void Update(Route entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new System.NotImplementedException();
        }
    }
}