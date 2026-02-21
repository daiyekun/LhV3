CREATE TABLE [dbo].[BcInstance]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[LbId] [int] NULL,
[Name] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Code] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Unit] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[Price] [decimal] (28, 6) NULL,
[Pro] [tinyint] NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BcInstance] ADD CONSTRAINT [PK_BcInstance] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BcInstance] WITH NOCHECK ADD CONSTRAINT [FK_BcInstance_UserInfor] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[BcInstance] WITH NOCHECK ADD CONSTRAINT [FK_BcInstance_BusinessCategory] FOREIGN KEY ([LbId]) REFERENCES [dbo].[BusinessCategory] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[BcInstance] WITH NOCHECK ADD CONSTRAINT [FK_BcInstance_UserInfor1] FOREIGN KEY ([ModifyUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[BcInstance] NOCHECK CONSTRAINT [FK_BcInstance_UserInfor]
GO
ALTER TABLE [dbo].[BcInstance] NOCHECK CONSTRAINT [FK_BcInstance_BusinessCategory]
GO
ALTER TABLE [dbo].[BcInstance] NOCHECK CONSTRAINT [FK_BcInstance_UserInfor1]
GO
