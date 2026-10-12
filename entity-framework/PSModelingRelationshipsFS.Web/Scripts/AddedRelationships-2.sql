BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesAgent]') AND [c].[name] = N'DisplayName');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [SalesAgent] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [SalesAgent] ALTER COLUMN [DisplayName] varchar(200) NOT NULL;

CREATE TABLE [Suplier] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_Suplier] PRIMARY KEY ([Id])
);

CREATE TABLE [Tag] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY ([Id])
);

CREATE TABLE [ProductSupliers] (
    [ProductId] int NOT NULL,
    [SuplierId] int NOT NULL,
    [ContractPrice] decimal(18,2) NOT NULL,
    [LeadTimeDays] int NOT NULL,
    [IsPreferred] bit NOT NULL,
    CONSTRAINT [PK_ProductSupliers] PRIMARY KEY ([ProductId], [SuplierId]),
    CONSTRAINT [FK_ProductSupliers_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductSupliers_Suplier_SuplierId] FOREIGN KEY ([SuplierId]) REFERENCES [Suplier] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [ProductTags] (
    [ProductId] int NOT NULL,
    [TagId] int NOT NULL,
    CONSTRAINT [PK_ProductTags] PRIMARY KEY ([ProductId], [TagId]),
    CONSTRAINT [FK_ProductTags_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductTags_Tag_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tag] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_ProductSupliers_SuplierId] ON [ProductSupliers] ([SuplierId]);

CREATE INDEX [IX_ProductTags_TagId] ON [ProductTags] ([TagId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261012231040_AddedRelationships-2', N'10.0.12');

COMMIT;
GO

