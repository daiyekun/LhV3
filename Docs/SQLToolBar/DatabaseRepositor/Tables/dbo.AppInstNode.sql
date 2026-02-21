CREATE TABLE [dbo].[AppInstNode]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[InstId] [int] NULL,
[TempHistId] [int] NULL,
[NodeStrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Name] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Left] [int] NULL,
[Top] [int] NULL,
[Type] [int] NULL,
[Width] [int] NULL,
[Height] [int] NULL,
[Alt] [tinyint] NULL,
[Marked] [tinyint] NULL,
[Norder] [int] NULL,
[NodeState] [int] NOT NULL,
[ReceDateTime] [datetime] NULL,
[CompDateTime] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppInstNode] ADD CONSTRAINT [PK_AppInstNode] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
