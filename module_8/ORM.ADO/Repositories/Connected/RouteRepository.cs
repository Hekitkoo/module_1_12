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
            var command = CreateCommand(RouteConstants.CreateQuery);

            command.Parameters.AddWithValue(nameof(Route.OriginWarehouseId), entity.OriginWarehouseId);
            command.Parameters.AddWithValue(nameof(Route.DestinationWarehouseId), entity.DestinationWarehouseId);
            command.Parameters.AddWithValue(nameof(Route.Distance), entity.Distance);

            entity.Id = Convert.ToInt32(command.ExecuteScalar());
        }

        public Route GetById(int entityId)
        {
            var command = CreateCommand(RouteConstants.GetByIdQuery);
            command.Parameters.AddWithValue(RouteConstants.RouteIdKey, entityId);

            using var reader = command.ExecuteReader();
            reader.Read();

            return new Route
            {
                Id = Convert.ToInt32(reader[RouteConstants.RouteIdKey]),
                OriginWarehouseId = Convert.ToInt32(reader[nameof(Route.OriginWarehouseId)]),
                DestinationWarehouseId = Convert.ToInt32(reader[nameof(Route.OriginWarehouseId)]),
                Distance = Convert.ToDouble(reader[nameof(Route.Distance)])
            };
        }

        public IEnumerable<Route> GetAll()
        {
            var command = CreateCommand(RouteConstants.GetAllQuery);

            using var reader = command.ExecuteReader();
            reader.Read();
            while (reader.Read())
            {
                yield return new Route
                {
                    Id = Convert.ToInt32(reader[RouteConstants.RouteIdKey]),
                    OriginWarehouseId = Convert.ToInt32(reader[nameof(Route.OriginWarehouseId)]),
                    DestinationWarehouseId = Convert.ToInt32(reader[nameof(Route.OriginWarehouseId)]),
                    Distance = Convert.ToDouble(reader[nameof(Route.Distance)])
                };
            }
        }

        public void Update(Route entity)
        {
            var command = CreateCommand(RouteConstants.UpdateQuery);

            command.Parameters.AddWithValue(nameof(Route.OriginWarehouseId), entity.OriginWarehouseId);
            command.Parameters.AddWithValue(nameof(Route.DestinationWarehouseId), entity.DestinationWarehouseId);
            command.Parameters.AddWithValue(nameof(Route.Distance), entity.Distance);
            command.Parameters.AddWithValue(RouteConstants.RouteIdKey, entity.Id);

            command.ExecuteNonQuery();
        }

        public void Delete(int entityId)
        {
            var command = CreateCommand(RouteConstants.DeleteQuery);
            command.Parameters.AddWithValue(RouteConstants.RouteIdKey, entityId);

            command.ExecuteNonQuery();
        }
    }
}