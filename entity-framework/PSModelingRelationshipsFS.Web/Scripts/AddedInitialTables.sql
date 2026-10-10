IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Customers] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(200) NOT NULL,
    [Email] varchar(320) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
);

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Sku] varchar(32) NOT NULL,
    [Name] varchar(200) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Products_Price_NonNegative] CHECK ([Price] >= 0)
);

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [CreatedUtc] datetime2 NOT NULL,
    [CustomerId] int NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [OrderLines] (
    [OrderId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Id] int NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_OrderLines] PRIMARY KEY ([OrderId], [ProductId]),
    CONSTRAINT [FK_OrderLines_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderLines_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);

CREATE UNIQUE INDEX [IX_Customers_Email] ON [Customers] ([Email]);

CREATE INDEX [IX_OrderLines_ProductId] ON [OrderLines] ([ProductId]);

CREATE INDEX [IX_Orders_CustomerId] ON [Orders] ([CustomerId]);

CREATE INDEX [IX_Products_Sku] ON [Products] ([Sku]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261010002947_AddedInitialTables', N'10.0.12');

COMMIT;
GO

