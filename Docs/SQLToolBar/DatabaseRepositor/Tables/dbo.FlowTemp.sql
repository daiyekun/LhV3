CREATE TABLE [dbo].[FlowTemp]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
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
ALTER TABLE [dbo].[FlowTemp] ADD CONSTRAINT [PK_FlowTemp] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FlowTemp] WITH NOCHECK ADD CONSTRAINT [FK_FlowTemp_UserInfor] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[FlowTemp] NOCHECK CONSTRAINT [FK_FlowTemp_UserInfor]
GO
