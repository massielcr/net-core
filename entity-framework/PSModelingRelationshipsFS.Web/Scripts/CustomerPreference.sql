BEGIN TRANSACTION;
ALTER TABLE [Customers] ADD [Preferences] json NOT NULL DEFAULT '{}';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261013043631_CustomerPreference', N'10.0.12');

COMMIT;
GO

