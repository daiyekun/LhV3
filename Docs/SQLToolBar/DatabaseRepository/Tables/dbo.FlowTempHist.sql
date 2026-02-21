CREATE TABLE [dbo].[FlowTempHist]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TempId] [int] NULL,
[Name] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Version] [int] NULL,
[IsValid] [tinyint] NULL,
[ObjType] [int] NULL,
[CreateUserId] [int] NULL,
[CreateDateTime] [datetime] NULL,
[IsDelete] [tinyint] NULL,
[DeptIds] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[CategoryIds] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[FlowItems] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FlowTempHist] ADD CONSTRAINT [PK_FlowTempHist] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FlowTempHist] WITH NOCHECK ADD CONSTRAINT [FK_FlowTempHist_UserInfor] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[FlowTempHist] NOCHECK CONSTRAINT [FK_FlowTempHist_UserInfor]
GO
