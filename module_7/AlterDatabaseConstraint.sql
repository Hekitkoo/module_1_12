USE SqlModule
GO

ALTER TABLE [dbo].[Shipment]  DROP
   CONSTRAINT [fk_Shipment_Route]
GO
ALTER TABLE [dbo].[Shipment]  ADD
   CONSTRAINT [fk_Shipment_Route]  
      FOREIGN KEY ([RouteId])
      REFERENCES [dbo].[Route]([RouteId])
      ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cargo]  DROP
   CONSTRAINT [fk_Cargo_Shipment]
GO
ALTER TABLE [dbo].[Cargo]  ADD
   CONSTRAINT [fk_Cargo_Shipment]  
      FOREIGN KEY ([ShipmentId])
      REFERENCES [dbo].[Shipment]([ShipmentId])
      ON DELETE CASCADE
GO

