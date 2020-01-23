CREATE TABLE [dbo].[Mushrooms] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [LatinName] NVARCHAR (64)  NOT NULL,
    [Info]      NVARCHAR (450) NOT NULL,
    [ImageUrl]  NVARCHAR (450) NOT NULL,
    [Edible]    BIT            NOT NULL,
    [Rating]    INT            Null,
    [Name]      NVARCHAR (64)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

