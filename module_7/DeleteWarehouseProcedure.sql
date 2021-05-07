USE SqlModule
GO

CREATE PROCEDURE [dbo].[DeleteWarehouse](@warehouseId INT)
AS BEGIN

DECLARE @cascadeRoutesToDelete TABLE (Id INT);

INSERT INTO @cascadeRoutesToDelete
	SELECT RouteId
	FROM [dbo].[Route]
	WHERE DestinationWarehouseId = @warehouseId
	OR OriginWarehouseId = @warehouseId;

DELETE
	FROM [dbo].[Route]
	WHERE RouteId IN (SELECT * FROM @cascadeRoutesToDelete);

DELETE
	FROM [dbo].[Warehouse]
	WHERE WarehouseId = @warehouseId;

END


/*TESTS*/
/*transaction to delete Warehouses with WarehouseId = 8, 16 and 24. */
BEGIN TRANSACTION
	EXEC [dbo].[DeleteWarehouse] 8;
	EXEC [dbo].[DeleteWarehouse] 16;
	EXEC [dbo].[DeleteWarehouse] 24;
COMMIT TRANSACTION

/*rollback deletion of WarehouseId 16 and 24, but commit removing of WarehouseId = 8.*/
BEGIN TRANSACTION rollbackMagick
	EXEC DeleteWarehouse 8;
	SAVE TRANSACTION rollbackMagick
		EXEC DeleteWarehouse 16;
		EXEC DeleteWarehouse 24;
	ROLLBACK TRANSACTION rollbackMagick
COMMIT TRANSACTION rollbackMagick
