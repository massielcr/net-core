BEGIN TRANSACTION;
ALTER TABLE [CustomerProfile] DROP CONSTRAINT [FK_CustomerProfile_Customers_CustomerId];

ALTER TABLE [Orders] DROP CONSTRAINT [FK_Orders_SalesAgent_SalesAgentId];

ALTER TABLE [ProductSupliers] DROP CONSTRAINT [FK_ProductSupliers_Suplier_SuplierId];

ALTER TABLE [ProductTags] DROP CONSTRAINT [FK_ProductTags_Tag_TagId];

ALTER TABLE [Tag] DROP CONSTRAINT [PK_Tag];

ALTER TABLE [Suplier] DROP CONSTRAINT [PK_Suplier];

ALTER TABLE [SalesAgent] DROP CONSTRAINT [PK_SalesAgent];

ALTER TABLE [CustomerProfile] DROP CONSTRAINT [PK_CustomerProfile];

EXEC sp_rename N'[Tag]', N'Tags', 'OBJECT';

EXEC sp_rename N'[Suplier]', N'Supliers', 'OBJECT';

EXEC sp_rename N'[SalesAgent]', N'SalesAgents', 'OBJECT';

EXEC sp_rename N'[CustomerProfile]', N'CustomerProfiles', 'OBJECT';

ALTER TABLE [Products] ADD [AutoRenews] bit NULL;

ALTER TABLE [Products] ADD [BillingPeriodMonths] int NULL;

ALTER TABLE [Products] ADD [DimHeightCm] decimal(9,2) NULL;

ALTER TABLE [Products] ADD [DimLenghtCm] decimal(9,2) NULL;

ALTER TABLE [Products] ADD [DimWidthCm] decimal(9,2) NULL;

ALTER TABLE [Products] ADD [DownloadUrl] nvarchar(max) NULL;

ALTER TABLE [Products] ADD [FileSizeBytes] bigint NULL;

ALTER TABLE [Products] ADD [Manufacturer] nvarchar(max) NULL;

ALTER TABLE [Products] ADD [ProductKind] nvarchar(13) NOT NULL DEFAULT N'';

ALTER TABLE [Products] ADD [RequiresSignature] bit NULL;

ALTER TABLE [Products] ADD [TrialEndsOn] date NULL;

ALTER TABLE [Products] ADD [WeigthKg] decimal(18,2) NULL;

ALTER TABLE [Tags] ADD CONSTRAINT [PK_Tags] PRIMARY KEY ([Id]);

ALTER TABLE [Supliers] ADD CONSTRAINT [PK_Supliers] PRIMARY KEY ([Id]);

ALTER TABLE [SalesAgents] ADD CONSTRAINT [PK_SalesAgents] PRIMARY KEY ([Id]);

ALTER TABLE [CustomerProfiles] ADD CONSTRAINT [PK_CustomerProfiles] PRIMARY KEY ([CustomerId]);

ALTER TABLE [CustomerProfiles] ADD CONSTRAINT [FK_CustomerProfiles_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE;

ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_SalesAgents_SalesAgentId] FOREIGN KEY ([SalesAgentId]) REFERENCES [SalesAgents] ([Id]) ON DELETE SET NULL;

ALTER TABLE [ProductSupliers] ADD CONSTRAINT [FK_ProductSupliers_Supliers_SuplierId] FOREIGN KEY ([SuplierId]) REFERENCES [Supliers] ([Id]) ON DELETE NO ACTION;

ALTER TABLE [ProductTags] ADD CONSTRAINT [FK_ProductTags_Tags_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tags] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261013040929_InheritanceMappingStrategy', N'10.0.12');

COMMIT;
GO

