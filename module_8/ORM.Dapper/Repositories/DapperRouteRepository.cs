using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ORM.Core.Interfaces;
using ORM.Core.Models;
using DapperExtensions;

namespace ORM.Dapper.Repositories
{
    public class DapperRouteRepository : IRepository<Route, int>
    {
        private readonly string connectionString;

        public DapperRouteRepository(string connectionString)
        {
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new[] { typeof(RouteMappedMapper).Assembly });
            this.connectionString = connectionString;
        }

        public void Create(Route entity)
        {
            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            dbConnection.Insert(entity);
            dbConnection.Close();
        }

        public Route GetById(int entityId)
        {
            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            var route = dbConnection.Get<Route>(entityId);
            dbConnection.Close();

            return route;
        }

        public IEnumerable<Route> GetAll()
        {
            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            var routes = dbConnection.GetList<Route>().ToList();
            dbConnection.Close();

            return routes;
        }

        public void Update(Route entity)
        {
            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            dbConnection.Update(entity);
            dbConnection.Close();
        }

        public void Delete(int entityId)
        {
            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            var route = dbConnection.Get<Route>(entityId);
            dbConnection.Delete(route);
            dbConnection.Close();
        }
    }
}