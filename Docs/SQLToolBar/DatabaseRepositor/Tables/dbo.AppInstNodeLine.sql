CREATE TABLE [dbo].[AppInstNodeLine]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[StrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[InstId] [int] NULL,
[Name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Type] [int] NULL,
[From] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[To] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Dash] [tinyint] NULL,
[Marked] [tinyint] NULL,
[Alt] [tinyint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppInstNodeLine] ADD CONSTRAINT [PK_AppInstNodeLine] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
