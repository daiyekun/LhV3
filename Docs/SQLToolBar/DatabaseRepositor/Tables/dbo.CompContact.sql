CREATE TABLE [dbo].[CompContact]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CompanyId] [int] NULL,
[Name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
[DeptName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Position] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Tel] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Mobile] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Fax] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Email] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Im] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompContact] ADD CONSTRAINT [PK_CompContact] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_CompContact] ON [dbo].[CompContact] ([CompanyId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompContact] WITH NOCHECK ADD CONSTRAINT [FK_CompContact_UserInfor1] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[CompContact] WITH NOCHECK ADD CONSTRAINT [FK_CompContact_UserInfor2] FOREIGN KEY ([ModifyUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[CompContact] NOCHECK CONSTRAINT [FK_CompContact_UserInfor1]
GO
ALTER TABLE [dbo].[CompContact] NOCHECK CONSTRAINT [FK_CompContact_UserInfor2]
GO
