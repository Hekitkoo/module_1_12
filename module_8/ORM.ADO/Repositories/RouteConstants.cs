namespace ORM.ADO.Repositories
{
    public static class RouteConstants
    {
        public const string RouteIdKey = "RouteId";

        public const string CreateQuery =
            @"INSERT INTO [dbo].[Route](OriginWarehouseId, DestinationWarehouseId, Distance) 
            OUTPUT INSERTED.RouteId values (@OriginWarehouseId, @DestinationWarehouseId, @Distance)";

        public const string GetByIdQuery = "SELECT * FROM  [dbo].[Route] WITH(NOLOCK) WHERE RouteId  = @RouteId";

        public const string GetAllQuery = "SELECT * FROM  [dbo].[Route] WITH(NOLOCK)";

        public const string UpdateQuery =
            @"UPDATE [dbo].[Route] SET OriginWarehouseId = @OriginWarehouseId, DestinationWarehouseId = @DestinationWarehouseId,
                         Distance = @Distance WHERE RouteId  = @RouteId";

        public const string DeleteQuery = "DELETE FROM [dbo].[Route] WHERE RouteId = @RouteId";
    }
}