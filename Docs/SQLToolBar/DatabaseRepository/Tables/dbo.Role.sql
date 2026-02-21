CREATE TABLE [dbo].[Role]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[No] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
[CreateUserId] [int] NOT NULL,
[CreateDatetime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDatetime] [datetime] NOT NULL,
[Rstate] [int] NOT NULL,
[DepartmentId] [int] NULL,
[Remark] [nvarchar] (1500) COLLATE Chinese_PRC_CI_AS NULL,
[IsDelete] [tinyint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Role] ADD CONSTRAINT [PK_Role_1] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Role] WITH NOCHECK ADD CONSTRAINT [FK_Role_UserInfor1] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Role] WITH NOCHECK ADD CONSTRAINT [FK_Role_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Role] WITH NOCHECK ADD CONSTRAINT [FK_Role_UserInfor2] FOREIGN KEY ([ModifyUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Role] NOCHECK CONSTRAINT [FK_Role_UserInfor1]
GO
ALTER TABLE [dbo].[Role] NOCHECK CONSTRAINT [FK_Role_Department]
GO
ALTER TABLE [dbo].[Role] NOCHECK CONSTRAINT [FK_Role_UserInfor2]
GO
