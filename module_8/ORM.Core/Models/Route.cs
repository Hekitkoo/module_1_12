namespace ORM.Core.Models
{
    public class Route : Base
    {
        public Warehouse OriginWarehouse { get; set; }
        public int OriginWarehouseId { get; set; }
        public Warehouse DestinationWarehouse { get; set; }
        public int DestinationWarehouseId { get; set; }
        public double Distance { get; set; }
    }
}