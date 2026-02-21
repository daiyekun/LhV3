CREATE TABLE [dbo].[FlowTempNodeInfoHist]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TempHistId] [int] NULL,
[TempId] [int] NULL,
[NodeStrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Nrule] [int] NULL,
[ReviseText] [int] NULL,
[GroupId] [int] NULL,
[Min] [decimal] (28, 6) NULL,
[Max] [decimal] (28, 6) NULL,
[IsMin] [int] NULL,
[IsMax] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FlowTempNodeInfoHist] ADD CONSTRAINT [PK_FlowTempNodeInfoHist] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FlowTempNodeInfoHist] WITH NOCHECK ADD CONSTRAINT [FK_FlowTempNodeInfoHist_GroupInfo] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[GroupInfo] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[FlowTempNodeInfoHist] NOCHECK CONSTRAINT [FK_FlowTempNodeInfoHist_GroupInfo]
GO
