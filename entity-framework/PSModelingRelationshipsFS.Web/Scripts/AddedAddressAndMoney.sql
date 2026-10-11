BEGIN TRANSACTION;
ALTER TABLE [Products] ADD [PriceCurrency] char(3) NOT NULL DEFAULT 'USD';

ALTER TABLE [Orders] ADD [ShipToCity] varchar(100) NOT NULL DEFAULT '';

ALTER TABLE [Orders] ADD [ShipToCountryCode] char(2) NOT NULL DEFAULT '';

ALTER TABLE [Orders] ADD [ShipToPostalCode] varchar(20) NOT NULL DEFAULT '';

ALTER TABLE [Orders] ADD [ShipToStreet] varchar(200) NOT NULL DEFAULT '';

ALTER TABLE [OrderLines] ADD [UnitPriceCurrency] char(3) NOT NULL DEFAULT 'USD';

ALTER TABLE [Customers] ADD [BillCity] varchar(100) NULL;

ALTER TABLE [Customers] ADD [BillCountryCode] char(2) NULL;

ALTER TABLE [Customers] ADD [BillPostalCode] varchar(20) NULL;

ALTER TABLE [Customers] ADD [BillStreet] varchar(200) NULL;

ALTER TABLE [Customers] ADD [BillingAddress_BillingPresent] bit NULL;

ALTER TABLE [Customers] ADD [ShipCity] varchar(100) NOT NULL DEFAULT '';

ALTER TABLE [Customers] ADD [ShipCountryCode] char(2) NOT NULL DEFAULT '';

ALTER TABLE [Customers] ADD [ShipPostalCode] varchar(20) NOT NULL DEFAULT '';

ALTER TABLE [Customers] ADD [ShipStreet] varchar(200) NOT NULL DEFAULT '';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261011025043_AddedAddressAndMoney', N'10.0.12');

COMMIT;
GO

