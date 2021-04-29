IF DB_ID('SqlModule') IS NOT NULL 
BEGIN
USE Master;
DROP DATABASE [SqlModule];
END

CREATE DATABASE [SqlModule];
GO

USE [SqlModule];

CREATE TABLE [dbo].[Warehouse] (
    [WarehouseId] INT IDENTITY(1, 1) NOT NULL,
    [City] NVARCHAR(50) NOT NULL,
    [State] NVARCHAR(50) NOT NULL,
    CONSTRAINT [pk_Warehouse] PRIMARY KEY CLUSTERED([WarehouseId])
);

CREATE TABLE [dbo].[Contact] (
    [ContactId] INT IDENTITY(1, 1) NOT NULL,
    [FirstName] NVARCHAR(50) NOT NULL,
    [LastName] NVARCHAR(50) NOT NULL,
    [CellPhone] NVARCHAR(50) NOT NULL,
    CONSTRAINT [pk_Contact] PRIMARY KEY CLUSTERED([ContactId])
);

CREATE TABLE [dbo].[Truck] (
    [TruckId] INT IDENTITY(1, 1) NOT NULL,
    [Brand] NVARCHAR(50) NOT NULL,
    [RegistrationNumber] NVARCHAR(50) NOT NULL,
    [Year] INT NULL,
    [Payload] FLOAT NOT NULL,
    [FuelConsumption] FLOAT NOT NULL,
    [Volume] FLOAT NOT NULL,
    CONSTRAINT [pk_Truck] PRIMARY KEY CLUSTERED([TruckId])
);

CREATE TABLE [dbo].[Driver] (
    [DriverId] INT IDENTITY(1, 1) NOT NULL,
    [FirstName] NVARCHAR(50) NOT NULL,
    [LastName] NVARCHAR(50) NOT NULL,
    [Birthdate] DATETIME NOT NULL,
    CONSTRAINT [pk_Driver] PRIMARY KEY CLUSTERED([DriverId])
);

CREATE TABLE [dbo].[DriverTruck] (
    [TruckId] INT NOT NULL,
    [DriverId] INT NOT NULL,
    CONSTRAINT [pk_DriverTruck] PRIMARY KEY ([DriverId], [TruckId]),
    CONSTRAINT [fk_DriverTruck_Driver] FOREIGN KEY ([DriverId]) REFERENCES [dbo].[Driver]([DriverId]),
    CONSTRAINT [fk_DriverTruck_Truck] FOREIGN KEY ([TruckId]) REFERENCES [dbo].[Truck]([TruckId])
);

CREATE TABLE [dbo].[Route] (
    [RouteId] INT IDENTITY(1, 1) NOT NULL,
    [OriginWarehouseId] INT NOT NULL,
    [DestinationWarehouseId] INT NOT NULL,
    [Distance] FLOAT NOT NULL,
    CONSTRAINT [pk_Route] PRIMARY KEY CLUSTERED([RouteId]),
    CONSTRAINT [fk_Route_Warehouse_Original] FOREIGN KEY ([OriginWarehouseId]) REFERENCES [dbo].[Warehouse]([WarehouseId]),
    CONSTRAINT [fk_Route_Warehouse_Destination] FOREIGN KEY ([DestinationWarehouseId]) REFERENCES [dbo].[Warehouse]([WarehouseId])
);

CREATE TABLE [dbo].[Shipment] (
    [ShipmentId] INT IDENTITY(1, 1) NOT NULL,
    [RouteId] INT NOT NULL,
    [DriverId] INT NOT NULL,
    [TruckId] INT NOT NULL,
    [DepartureDate] DATETIME NOT NULL,
    [DeliveryDate] DATETIME NOT NULL,
    CONSTRAINT [pk_Shipment] PRIMARY KEY CLUSTERED([ShipmentId]),
    CONSTRAINT [fk_Shipment_Route] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Route]([RouteId]),
    CONSTRAINT [fk_Shipment_Driver] FOREIGN KEY ([DriverId]) REFERENCES [dbo].[Driver]([DriverId]),
    CONSTRAINT [fk_Shipment_Truck] FOREIGN KEY ([TruckId]) REFERENCES [dbo].[Truck]([TruckId])
);


CREATE TABLE [dbo].[Cargo] (
    [CargoId] INT IDENTITY(1, 1) NOT NULL,
    [Weight] DECIMAL NOT NULL,
    [Volume] FLOAT NOT NULL,
    [SenderId] INT NOT NULL,
    [RecipientId] INT NOT NULL,
    [ShipmentId] INT NULL,
    CONSTRAINT [pk_Cargo] PRIMARY KEY CLUSTERED([CargoId]),
    CONSTRAINT [fk_Cargo_Sender] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Contact]([ContactId]),
    CONSTRAINT [fk_Cargo_Recipient] FOREIGN KEY ([RecipientId]) REFERENCES [dbo].[Contact]([ContactId]),
    CONSTRAINT [fk_Cargo_Shipment] FOREIGN KEY ([ShipmentId]) REFERENCES [dbo].[Shipment]([ShipmentId]),
);
