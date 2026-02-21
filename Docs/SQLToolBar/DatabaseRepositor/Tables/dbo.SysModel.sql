CREATE TABLE [dbo].[SysModel]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[PId] [int] NULL,
[Name] [nvarchar] (30) COLLATE Chinese_PRC_CI_AS NULL,
[No] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[ControllerName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[ActionName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Sort] [int] NULL,
[RequestUrl] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[IsShow] [tinyint] NULL,
[AreaName] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[IsDelete] [tinyint] NULL,
[MPath] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Leaf] [int] NULL,
[CreateUserId] [int] NULL,
[CreateDatetime] [datetime] NULL,
[ModifyUserId] [int] NULL,
[ModifyDatetime] [datetime] NULL,
[Ico] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysModel] ADD CONSTRAINT [PK_SysModel] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysModel] WITH NOCHECK ADD CONSTRAINT [FK_SysModel_UserInfor1] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[SysModel] WITH NOCHECK ADD CONSTRAINT [FK_SysModel_UserInfor2] FOREIGN KEY ([ModifyUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[SysModel] NOCHECK CONSTRAINT [FK_SysModel_UserInfor1]
GO
ALTER TABLE [dbo].[SysModel] NOCHECK CONSTRAINT [FK_SysModel_UserInfor2]
GO
