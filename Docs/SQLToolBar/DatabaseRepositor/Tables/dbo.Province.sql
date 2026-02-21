CREATE TABLE [dbo].[Province]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CountryId] [int] NULL,
[Name] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[DisplayName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[IsEnable] [tinyint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Province] ADD CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
