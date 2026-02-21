CREATE TABLE [dbo].[AppInstNodeLineHist]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[StrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[InstHistId] [int] NULL,
[Name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Type] [int] NULL,
[From] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[To] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Dash] [tinyint] NULL,
[Marked] [tinyint] NULL,
[Alt] [tinyint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppInstNodeLineHist] ADD CONSTRAINT [PK_AppInstNodeLineHist] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
