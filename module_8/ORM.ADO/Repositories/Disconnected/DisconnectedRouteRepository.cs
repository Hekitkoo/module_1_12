using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ORM.Core.Interfaces;
using ORM.Core.Models;

namespace ORM.ADO.Repositories.Disconnected
{
    public class DisconnectedRouteRepository : Repository, IRepository<Route, int>, IDisconnectedRepository
    {
        private SqlDataAdapter sqlDataAdapter;
        private readonly DataSet routeDataSet;
        private readonly string routeTableName;

        public DisconnectedRouteRepository(SqlTransaction transaction, SqlConnection context) 
            : base(transaction, context)
        {
            sqlDataAdapter = new SqlDataAdapter();
            routeDataSet = new DataSet();
            routeTableName = nameof(Route);
            LoadData();
        }
        
        public void Create(Route entity)
        {
            var newRow = routeDataSet.Tables[routeTableName]?.NewRow();
            newRow[nameof(Route.DestinationWarehouseId)] = entity.OriginWarehouseId;
            newRow[nameof(Route.OriginWarehouseId)] = entity.DestinationWarehouseId;
            newRow[nameof(Route.Distance)] = entity.Distance;
            newRow["RouteId"] = entity.Id;
            
            var commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            sqlDataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
            routeDataSet.Tables[routeTableName]?.Rows.Add(newRow);
            sqlDataAdapter.Update(routeDataSet, routeTableName);
        }

        public Route GetById(int entityId)
        {
            var drFind = routeDataSet.Tables[routeTableName]?.Rows.Find(entityId);
            
            return new Route
            {
                Id = Convert.ToInt32(drFind?["RouteId"]),
                OriginWarehouseId = Convert.ToInt32(drFind?[nameof(Route.OriginWarehouseId)]),
                DestinationWarehouseId = Convert.ToInt32(drFind?[nameof(Route.OriginWarehouseId)]),
                Distance = Convert.ToDouble(drFind?[nameof(Route.Distance)])
            };
        }

        public IEnumerable<Route> GetAll()
        {
            return from DataRow row in routeDataSet.Tables[routeTableName]?.Rows select new Route
            {
                Id = Convert.ToInt32(row["RouteId"]),
                OriginWarehouseId = Convert.ToInt32(row[nameof(Route.OriginWarehouseId)]),
                DestinationWarehouseId = Convert.ToInt32(row[nameof(Route.OriginWarehouseId)]),
                Distance = Convert.ToDouble(row[nameof(Route.Distance)])
            };
        }

        public void Update(Route entity)
        {
            var drFind = routeDataSet.Tables[routeTableName]?.Rows.Find(entity.Id);
            
            drFind[nameof(Route.OriginWarehouseId)] = entity.OriginWarehouseId;
            drFind[nameof(Route.DestinationWarehouseId)] = entity.DestinationWarehouseId;
            drFind[nameof(Route.Distance)] = entity.Distance;
            var commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            sqlDataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
        }

        public void Delete(int entityId)
        {
            var drFind = routeDataSet.Tables[routeTableName].Rows.Find(entityId);
            
            drFind?.Delete();
            var commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            sqlDataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();
        }

        public void LoadData()
        {
            var command = CreateCommand("SELECT * FROM  [dbo].[Route] WITH(NOLOCK)");
            sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(routeDataSet, routeTableName);
            var columns = new[] { routeDataSet.Tables[routeTableName]?.Columns["RouteId"] };
            routeDataSet.Tables[routeTableName].PrimaryKey = columns;
        }
    }
}