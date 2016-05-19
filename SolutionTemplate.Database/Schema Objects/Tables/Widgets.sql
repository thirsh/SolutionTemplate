CREATE TABLE [dbo].[Widgets] (
    [Id]      INT          NOT NULL PRIMARY KEY CLUSTERED IDENTITY,
    [Name]    VARCHAR (50) NOT NULL,
    [Active]  BIT          NOT NULL DEFAULT 0,
    [CreatedUtc] DATETIME     NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedUtc] DATETIME     NULL
);

GO

CREATE TRIGGER [dbo].[TR_Widgets_Updated]
    ON [dbo].[Widgets]
    FOR UPDATE
    AS
    BEGIN
        SET NoCount ON

		UPDATE [dbo].[Widgets]
		SET [UpdatedUtc] = GETUTCDATE()
		WHERE [Id] IN (SELECT DISTINCT [Id] FROM Inserted)
    END