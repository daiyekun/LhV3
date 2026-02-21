CREATE TABLE [dbo].[Country]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[DisplayName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[IsEnable] [tinyint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Country] ADD CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
