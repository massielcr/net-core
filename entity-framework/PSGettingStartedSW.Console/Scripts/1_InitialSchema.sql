BEGIN TRANSACTION;
CREATE TABLE [Actors] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Age] int NOT NULL,
    [Gender] int NOT NULL,
    [ImbLink] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Actors] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261006174122_CreateActorsTable', N'10.0.12');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Films]') AND [c].[name] = N'Title');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Films] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [Films] ALTER COLUMN [Title] nvarchar(50) NOT NULL;

ALTER TABLE [Actors] ADD [FilmId] int NULL;

CREATE INDEX [IX_Actors_FilmId] ON [Actors] ([FilmId]);

ALTER TABLE [Actors] ADD CONSTRAINT [FK_Actors_Films_FilmId] FOREIGN KEY ([FilmId]) REFERENCES [Films] ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261006174953_AddedActorsToFilm', N'10.0.12');

COMMIT;
GO

