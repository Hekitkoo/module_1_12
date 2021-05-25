using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ORM.Core.Interfaces;
using ORM.Core.Models;

namespace ORM.ADO.Repositories.Connected
{
    public class WarehouseRepository : Repository, IRepository<Warehouse, int>
    {
        public WarehouseRepository(SqlTransaction transaction, SqlConnection context)
            : base(transaction, context)
        {
        }

        public void Create(Warehouse entity)
        {
            const string query = "INSERT INTO [dbo].[Warehouse](City, State) OUTPUT INSERTED.WarehouseId values (@city, @state)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@city", entity.City);
            command.Parameters.AddWithValue("@state", entity.State);

            entity.Id = Convert.ToInt32(command.ExecuteScalar());
        }

        public Warehouse GetById(int entityId)
        {
            var command =
                CreateCommand("SELECT * FROM  [dbo].[Warehouse] WITH(NOLOCK) WHERE WarehouseId = @warehouseId");
            command.Parameters.AddWithValue("@warehouseId", entityId);

            using var reader = command.ExecuteReader();
            reader.Read();

            return new Warehouse
            {
                Id = Convert.ToInt32(reader["WarehouseId"]),
                City = reader[nameof(Warehouse.City)].ToString(),
                State = reader[nameof(Warehouse.State)].ToString()
            };
        }

        public IEnumerable<Warehouse> GetAll()
        {
            var command = CreateCommand("SELECT * FROM [dbo].[Warehouse] WITH(NOLOCK)");

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return
                    new Warehouse
                    {
                        Id = Convert.ToInt32(reader["WarehouseId"]),
                        City = reader[nameof(Warehouse.City)].ToString(),
                        State = reader[nameof(Warehouse.State)].ToString()
                    };
            }
        }

        public void Update(Warehouse entity)
        {
            const string query =
                "UPDATE [dbo].[Warehouse] SET City = @city, State = @state WHERE WarehouseId = @warehouseId";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@city", entity.City);
            command.Parameters.AddWithValue("@state", entity.State);
            command.Parameters.AddWithValue("@warehouseId", entity.Id);

            command.ExecuteNonQuery();
        }

        public void Delete(int entityId)
        {
            var command = CreateCommand("DELETE FROM [dbo].[Warehouse] WHERE WarehouseId = @warehouseId");
            command.Parameters.AddWithValue("@warehouseId", entityId);

            command.ExecuteNonQuery();
        }
    }
}