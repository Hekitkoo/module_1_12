USE [SqlModule];
GO

DELETE duplicatedTrucks FROM (
	SELECT *, ROW_NUMBER() OVER (PARTITION BY [RegistrationNumber] ORDER BY [TruckId] ASC) AS rowNumber
	FROM [dbo].[Truck] 
) AS duplicatedTrucks
WHERE duplicatedTrucks.rowNumber > 1;
GO

DELETE duplicatedTrucks FROM (
	SELECT *, COUNT(*) OVER (PARTITION BY RegistrationNumber) AS rowNumber, MIN([TruckId]) OVER (PARTITION BY RegistrationNumber) AS minTruckId
	FROM [dbo].[Truck]
) AS duplicatedTrucks
WHERE duplicatedTrucks.rowNumber > 1
AND [TruckId] != minTruckId;
GO