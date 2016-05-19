CREATE TABLE [dbo].[Widget] (
    [Id]      INT          IDENTITY (1, 1) NOT NULL,
    [Name]    VARCHAR (50) NOT NULL,
    [Active]  BIT          NOT NULL,
    [Created] DATETIME     NOT NULL,
    [Updated] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

