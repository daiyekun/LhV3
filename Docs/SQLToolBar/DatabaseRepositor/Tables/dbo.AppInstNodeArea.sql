CREATE TABLE [dbo].[AppInstNodeArea]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[StrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[InstId] [int] NULL,
[Name] [nvarchar] (150) COLLATE Chinese_PRC_CI_AS NULL,
[Left] [int] NULL,
[Top] [int] NULL,
[Width] [int] NULL,
[height] [int] NULL,
[Color] [int] NULL,
[Alt] [tinyint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppInstNodeArea] ADD CONSTRAINT [PK_AppInstNodeArea] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
