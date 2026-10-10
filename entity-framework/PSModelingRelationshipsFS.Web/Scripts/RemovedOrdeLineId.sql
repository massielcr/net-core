BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrderLines]') AND [c].[name] = N'Id');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [OrderLines] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [OrderLines] DROP COLUMN [Id];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261010003605_RemovedOrderLineId', N'10.0.12');

COMMIT;
GO

