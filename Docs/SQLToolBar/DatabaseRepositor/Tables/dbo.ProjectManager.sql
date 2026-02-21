CREATE TABLE [dbo].[ProjectManager]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Pid] [int] NULL,
[Name] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Code] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[CategoryId] [int] NOT NULL,
[PlanBeginDateTime] [datetime] NULL,
[PlanCompleteDateTime] [datetime] NULL,
[ActualBeginDateTime] [datetime] NULL,
[ActualCompleteDateTime] [datetime] NULL,
[BugetCollectAmountMoney] [decimal] (28, 6) NULL,
[BudgetCollectCurrencyId] [int] NULL,
[BudgetPayAmountMoney] [decimal] (28, 6) NULL,
[BudgetPayCurrencyId] [int] NULL,
[PrincipalUserId] [int] NULL,
[Pstate] [int] NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL,
[Reserve1] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Reserve2] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[WfState] [tinyint] NULL,
[WfItem] [int] NULL,
[WfCurrNodeName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProjectManager] ADD CONSTRAINT [PK_ProjectManager] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ProjectManager] ON [dbo].[ProjectManager] ([PrincipalUserId], [CreateUserId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProjectManager] WITH NOCHECK ADD CONSTRAINT [FK_ProjectManager_CurrencyManager1] FOREIGN KEY ([BudgetCollectCurrencyId]) REFERENCES [dbo].[CurrencyManager] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ProjectManager] WITH NOCHECK ADD CONSTRAINT [FK_ProjectManager_CurrencyManager2] FOREIGN KEY ([BudgetPayCurrencyId]) REFERENCES [dbo].[CurrencyManager] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ProjectManager] WITH NOCHECK ADD CONSTRAINT [FK_ProjectManager_DataDictionary] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ProjectManager] WITH NOCHECK ADD CONSTRAINT [FK_ProjectManager_UserInfor1] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ProjectManager] WITH NOCHECK ADD CONSTRAINT [FK_ProjectManager_UserInfor2] FOREIGN KEY ([ModifyUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ProjectManager] WITH NOCHECK ADD CONSTRAINT [FK_ProjectManager_UserInfor3] FOREIGN KEY ([PrincipalUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ProjectManager] NOCHECK CONSTRAINT [FK_ProjectManager_CurrencyManager1]
GO
ALTER TABLE [dbo].[ProjectManager] NOCHECK CONSTRAINT [FK_ProjectManager_CurrencyManager2]
GO
ALTER TABLE [dbo].[ProjectManager] NOCHECK CONSTRAINT [FK_ProjectManager_DataDictionary]
GO
ALTER TABLE [dbo].[ProjectManager] NOCHECK CONSTRAINT [FK_ProjectManager_UserInfor1]
GO
ALTER TABLE [dbo].[ProjectManager] NOCHECK CONSTRAINT [FK_ProjectManager_UserInfor2]
GO
ALTER TABLE [dbo].[ProjectManager] NOCHECK CONSTRAINT [FK_ProjectManager_UserInfor3]
GO
