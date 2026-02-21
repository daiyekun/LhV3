CREATE TABLE [dbo].[FlowTempNode]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[StrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[TempId] [int] NULL,
[Name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Left] [int] NULL,
[Top] [int] NULL,
[Type] [int] NULL,
[Height] [int] NULL,
[Width] [int] NULL,
[Alt] [tinyint] NULL,
[Marked] [tinyint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FlowTempNode] ADD CONSTRAINT [PK_FlowTempNode] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
