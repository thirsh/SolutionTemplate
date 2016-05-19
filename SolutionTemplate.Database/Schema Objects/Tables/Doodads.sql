CREATE TABLE [dbo].[Doodads]
(
	[Id]		INT				NOT NULL PRIMARY KEY CLUSTERED IDENTITY, 
    [WidgetId]	INT				NOT NULL, 
    [Name]		VARCHAR(50)		NOT NULL, 
    [Active]	BIT				NOT NULL DEFAULT 0,
	[CreatedUtc]	DATETIME		NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedUtc]	DATETIME		NULL, 
    CONSTRAINT [FK_Doodads_Widgets] FOREIGN KEY ([WidgetId]) REFERENCES [dbo].[Widgets]([Id])
)

GO

CREATE TRIGGER [dbo].[TR_Doodads_Updated]
    ON [dbo].[Doodads]
    FOR UPDATE
    AS
    BEGIN
        SET NoCount ON

		UPDATE [dbo].[Doodads]
		SET [UpdatedUtc] = GETUTCDATE()
		WHERE [Id] IN (SELECT DISTINCT [Id] FROM Inserted)
    END