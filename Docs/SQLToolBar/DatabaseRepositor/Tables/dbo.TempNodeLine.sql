CREATE TABLE [dbo].[TempNodeLine]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[StrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[TempId] [int] NULL,
[Name] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[Type] [int] NULL,
[From] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[To] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Dash] [tinyint] NULL,
[Marked] [tinyint] NULL,
[Alt] [tinyint] NULL,
[M] [float] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TempNodeLine] ADD CONSTRAINT [PK_TempNodeLine] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
