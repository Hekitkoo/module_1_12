USE [SqlModule];

GO

/* First View better */

CREATE VIEW [dbo].[vShipment_1]
AS
SELECT 
	originWarehouse.City AS OriginCity,
	destinationWarehouse.City AS DestinationCity,
	truck.Brand,
	shipment.DepartureDate,
	shipment.DeliveryDate,
	cargoResult.TotalWeight,
	cargoResult.TotalVolume,
	(route.Distance * truck.FuelConsumption) / 100 AS FuelSpent
FROM [dbo].[Shipment] shipment
	CROSS APPLY
	(
		SELECT SUM(cargo.Weight) AS TotalWeight, SUM(cargo.Volume) AS TotalVolume
		FROM [dbo].[Cargo] cargo
		WHERE shipment.ShipmentId = cargo.ShipmentId
		GROUP BY cargo.ShipmentId
	) cargoResult
	LEFT JOIN [dbo].[Route] route
		ON route.RouteId = shipment.RouteId
	LEFT JOIN [dbo].[Warehouse] originWarehouse
		ON originWarehouse.WarehouseId = route.OriginWarehouseId
	LEFT JOIN [dbo].[Warehouse] destinationWarehouse
		ON destinationWarehouse.WarehouseId = route.DestinationWarehouseId
	LEFT JOIN [dbo].[Truck] truck
		ON truck.TruckId = shipment.TruckId;

GO

/* Second View */

CREATE VIEW [dbo].[vShipment_2]
AS
WITH
CargoShipCTE (ShipmentId, TotalWeight, TotalVolume)
AS
(		
SELECT  
		cargo.ShipmentId,
		SUM(cargo.Weight) AS TotalWeight,
		SUM(cargo.Volume) AS TotalVolume
		FROM [dbo].[Shipment] AS shipment
		LEFT OUTER JOIN  [dbo].[Cargo] AS cargo
		ON shipment.ShipmentId = cargo.ShipmentId
		GROUP BY cargo.ShipmentId
)
SELECT 
	originWarehouse.City AS OriginCity,
	destinationWarehouse.City AS DestinationCity,
	truck.Brand,
	shipment.DepartureDate,
	shipment.DeliveryDate,
	TotalWeight,
	TotalVolume,
	(route.Distance * truck.FuelConsumption) / 100 AS FuelSpent
FROM CargoShipCTE
	LEFT JOIN [dbo].[Shipment] shipment
		ON CargoShipCTE.ShipmentId = shipment.ShipmentId
	LEFT JOIN [dbo].[Route] route
		ON route.RouteId = shipment.RouteId
	LEFT JOIN [dbo].[Warehouse] originWarehouse
		ON originWarehouse.WarehouseId = route.OriginWarehouseId
	LEFT JOIN [dbo].[Warehouse] destinationWarehouse
		ON destinationWarehouse.WarehouseId = route.DestinationWarehouseId
	LEFT JOIN [dbo].[Truck] truck
		ON truck.TruckId = shipment.TruckId

GO
