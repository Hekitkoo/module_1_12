/*Using Connection2 create Transaction2 with UPDATE statement to update Destination field in Route table where RouteId = 2.*/
BEGIN TRANSACTION Transaction2
	UPDATE [dbo].[Route]
	SET OriginWarehouseId = 6
	WHERE RouteId = 2;
COMMIT TRANSACTION Transaction2