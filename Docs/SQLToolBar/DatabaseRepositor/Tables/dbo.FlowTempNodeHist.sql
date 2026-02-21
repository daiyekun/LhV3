CREATE TABLE [dbo].[FlowTempNodeHist]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[StrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[TempHistId] [int] NULL,
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
ALTER TABLE [dbo].[FlowTempNodeHist] ADD CONSTRAINT [PK_FlowTempNodeHist] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
