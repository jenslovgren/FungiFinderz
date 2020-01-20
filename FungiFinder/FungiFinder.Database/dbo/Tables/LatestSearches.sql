CREATE TABLE [dbo].[LatestSearches] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [SearchDate] DATETIME       NOT NULL,
    [UserId]     NVARCHAR (450) NOT NULL,
    [Mushroom]   NCHAR (64)     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

