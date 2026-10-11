BEGIN TRANSACTION;
ALTER TABLE [Orders] DROP CONSTRAINT [FK_Orders_Customers_CustomerId];

ALTER TABLE [Orders] ADD [SalesAgentId] int NULL;

CREATE TABLE [CustomerProfile] (
    [CustomerId] int NOT NULL,
    [LoyaltyTier] nvarchar(max) NOT NULL,
    [DateOfBirthUtc] datetime2 NULL,
    CONSTRAINT [PK_CustomerProfile] PRIMARY KEY ([CustomerId]),
    CONSTRAINT [FK_CustomerProfile_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [SalesAgent] (
    [Id] int NOT NULL IDENTITY,
    [DisplayName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_SalesAgent] PRIMARY KEY ([Id])
);

CREATE INDEX [IX_Orders_SalesAgentId] ON [Orders] ([SalesAgentId]);

ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION;

ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_SalesAgent_SalesAgentId] FOREIGN KEY ([SalesAgentId]) REFERENCES [SalesAgent] ([Id]) ON DELETE SET NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261011033400_AddedRelationships', N'10.0.12');

COMMIT;
GO

