BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tag]') AND [c].[name] = N'Name');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Tag] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [Tag] ALTER COLUMN [Name] nvarchar(50) NOT NULL;

DECLARE @var1 nvarchar(max);
SELECT @var1 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerProfile]') AND [c].[name] = N'LoyaltyTier');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [CustomerProfile] DROP CONSTRAINT ' + @var1 + ';');
ALTER TABLE [CustomerProfile] ALTER COLUMN [LoyaltyTier] nvarchar(100) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261012232232_FixedDataTypes', N'10.0.12');

COMMIT;
GO

