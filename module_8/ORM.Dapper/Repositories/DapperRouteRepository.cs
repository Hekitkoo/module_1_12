using System.Collections.Generic;
using System.Data.SqlClient;
using ORM.Core.Interfaces;
using ORM.Core.Models;
using DapperExtensions;

namespace ORM.Dapper.Repositories
{
    public class DapperRouteRepository : IRepository<Route, int>
    {
        private readonly SqlConnection connection;

        public DapperRouteRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Create(Route entity)
        {
            using var sqlConnection = connection;
            connection.Open();
            connection.Insert(entity);
            connection.Close();
        }

        public Route GetById(int entityId)
        {
            using var sqlConnection = connection;
            
                connection.Open();
                var route = connection.Get<Route>(entityId);
                connection.Close();
            
            return route;
        }

        public IEnumerable<Route> GetAll()
        {
            using var sqlConnection = connection;
            connection.Open();
            var routes = sqlConnection.GetList<Route>();
            sqlConnection.Close();
            return routes;
        }

        public void Update(Route entity)
        {
            using var sqlConnection = connection;
            sqlConnection.Open();
            sqlConnection.Update(entity);
            sqlConnection.Close();
        }

        public void Delete(int entityId)
        {
            using var sqlConnection = connection;
            sqlConnection.Open();
            var route = connection.Get<Route>(entityId);
            sqlConnection.Delete(route);
            sqlConnection.Close();
        }
    }
}