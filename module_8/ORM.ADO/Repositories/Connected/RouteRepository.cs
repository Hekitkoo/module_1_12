using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ORM.Core.Interfaces;
using ORM.Core.Models;

namespace ORM.ADO.Repositories.Connected
{
    public class RouteRepository : Repository, IRepository<Route, int>
    {
        public RouteRepository(
            SqlTransaction transaction,
            SqlConnection context)
            : base(transaction, context)
        {
        }

        public void Create(Route entity)
        {
            const string query = @"INSERT INTO [dbo].[Route](OriginWarehouseId, DestinationWarehouseId, Distance) 
            OUTPUT INSERTED.RouteId values (@originWarehouseId, @destinationWarehouseId, @distance)";

            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@originWarehouseId", entity.OriginWarehouseId);
            command.Parameters.AddWithValue("@destinationWarehouseId", entity.DestinationWarehouseId);
            command.Parameters.AddWithValue("@distance", entity.Distance);

            entity.Id = Convert.ToInt32(command.ExecuteScalar());
        }

        public Route GetById(int entityId)
        {
            var command = CreateCommand("SELECT * FROM  [dbo].[Route] WITH(NOLOCK) WHERE RouteId  = @routeId");
            command.Parameters.AddWithValue("@routeId", entityId);

            using var reader = command.ExecuteReader();
            reader.Read();

            return new Route
            {
                Id = Convert.ToInt32(reader["RouteId"]),
                OriginWarehouseId = Convert.ToInt32(reader[nameof(Route.OriginWarehouseId)]),
                DestinationWarehouseId = Convert.ToInt32(reader[nameof(Route.OriginWarehouseId)]),
                Distance = Convert.ToDouble(reader[nameof(Route.Distance)])
            };
        }

        public IEnumerable<Route> GetAll()
        {
            var command = CreateCommand("SELECT * FROM  [dbo].[Route] WITH(NOLOCK)");

            using var reader = command.ExecuteReader();
            reader.Read();
            while (reader.Read())
            {
                yield return new Route
                {
                    Id = Convert.ToInt32(reader["RouteId"]),
                    OriginWarehouseId = Convert.ToInt32(reader[nameof(Route.OriginWarehouseId)]),
                    DestinationWarehouseId = Convert.ToInt32(reader[nameof(Route.OriginWarehouseId)]),
                    Distance = Convert.ToDouble(reader[nameof(Route.Distance)])
                };
            }
        }

        public void Update(Route entity)
        {
            const string query =
                @"UPDATE [dbo].[Route] SET OriginWarehouseId = @originWarehouseId, DestinationWarehouseId = @destinationWarehouseId,
                         Distance = @distance WHERE RouteId  = @routeId";

            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@originWarehouseId", entity.OriginWarehouseId);
            command.Parameters.AddWithValue("@destinationWarehouseId", entity.DestinationWarehouseId);
            command.Parameters.AddWithValue("@distance", entity.Distance);
            command.Parameters.AddWithValue("@routeId", entity.Id);

            command.ExecuteNonQuery();
        }

        public void Delete(int entityId)
        {
            var command = CreateCommand("DELETE FROM [dbo].[Route] WHERE RouteId = @routeId");
            command.Parameters.AddWithValue("@routeId", entityId);

            command.ExecuteNonQuery();
        }
    }
}