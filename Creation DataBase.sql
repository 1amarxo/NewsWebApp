
CREATE TABLE [dbo].[Category] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (60) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[NEWS] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Title]      NVARCHAR (100) NOT NULL,
    [Content]    TEXT           NOT NULL,
    [Date]       DATETIME       NOT NULL,
    [PosterUrl]  NVARCHAR (255) NULL,
    [CategoryId] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Tag] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (60) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[NewsTag] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [NewsId] INT NOT NULL,
    [TagId]  INT NOT NULL,
    FOREIGN KEY ([NewsId]) REFERENCES [dbo].[NEWS] ([Id]),
    FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id])
);
