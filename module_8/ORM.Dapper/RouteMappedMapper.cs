using DapperExtensions.Mapper;
using ORM.Core.Models;

namespace ORM.Dapper
{
    public class RouteMappedMapper : ClassMapper<Route>
    {
        public RouteMappedMapper()
        {
            Table(nameof(Route));
            Map(x => x.Id).Column("RouteId").Key(KeyType.Identity);
            Map(x => x.DestinationWarehouse).Ignore();
            Map(x => x.OriginWarehouse).Ignore();
            Map(x => x.OriginWarehouseId).Column(nameof(Route.OriginWarehouseId));
            Map(x => x.DestinationWarehouseId).Column(nameof(Route.DestinationWarehouseId));
            Map(x => x.Distance).Column(nameof(Route.Distance));
            AutoMap();
        }
    }
}