CREATE TABLE [dbo].[ContractInfoHistory]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ContId] [int] NULL,
[Code] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[OtherCode] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Name] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ContTypeId] [int] NULL,
[FinanceType] [tinyint] NOT NULL,
[AmountMoney] [decimal] (28, 6) NULL,
[StampTax] [decimal] (28, 6) NULL,
[CurrencyId] [int] NULL,
[CurrencyRate] [decimal] (18, 8) NULL,
[ContState] [tinyint] NOT NULL,
[DeptId] [int] NULL,
[CompId] [int] NULL,
[ProjectId] [int] NULL,
[SngnDateTime] [datetime] NULL,
[EffectiveDateTime] [datetime] NULL,
[PlanCompleteDateTime] [datetime] NULL,
[ActualCompleteDateTime] [datetime] NULL,
[PrincipalUserId] [int] NULL,
[FinanceTerms] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[ModificationTimes] [int] NULL,
[ModificationRemark] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL,
[Reserve1] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Reserve2] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[MainDeptId] [int] NULL,
[ContDivision] [tinyint] NULL,
[SumContId] [int] NULL,
[CompId3] [int] NULL,
[CompId4] [int] NULL,
[IsFramework] [tinyint] NULL,
[PerformanceDateTime] [datetime] NULL,
[AdvanceAmount] [decimal] (28, 6) NULL,
[EstimateAmount] [decimal] (28, 6) NULL,
[ContSourceId] [int] NULL,
[WfState] [tinyint] NULL,
[WfItem] [int] NULL,
[WfCurrNodeName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[ContSingNo] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContractInfoHistory] ADD CONSTRAINT [PK_ContractInfoHistory] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContractInfoHistory] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfoHistory_Company] FOREIGN KEY ([CompId]) REFERENCES [dbo].[Company] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfoHistory] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfoHistory_Company3] FOREIGN KEY ([CompId3]) REFERENCES [dbo].[Company] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfoHistory] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfoHistory_Company4] FOREIGN KEY ([CompId4]) REFERENCES [dbo].[Company] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfoHistory] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfoHistory_ContSource] FOREIGN KEY ([ContSourceId]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfoHistory] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfoHistory_ContType] FOREIGN KEY ([ContTypeId]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfoHistory] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfoHistory_CreateUser] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfoHistory] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfoHistory_Department] FOREIGN KEY ([DeptId]) REFERENCES [dbo].[Department] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfoHistory] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfoHistory_MainDepartment] FOREIGN KEY ([MainDeptId]) REFERENCES [dbo].[Department] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfoHistory] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfoHistory_PriUser] FOREIGN KEY ([PrincipalUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfoHistory] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfoHistory_ProjectManager] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[ProjectManager] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfoHistory] NOCHECK CONSTRAINT [FK_ContractInfoHistory_Company]
GO
ALTER TABLE [dbo].[ContractInfoHistory] NOCHECK CONSTRAINT [FK_ContractInfoHistory_Company3]
GO
ALTER TABLE [dbo].[ContractInfoHistory] NOCHECK CONSTRAINT [FK_ContractInfoHistory_Company4]
GO
ALTER TABLE [dbo].[ContractInfoHistory] NOCHECK CONSTRAINT [FK_ContractInfoHistory_ContSource]
GO
ALTER TABLE [dbo].[ContractInfoHistory] NOCHECK CONSTRAINT [FK_ContractInfoHistory_ContType]
GO
ALTER TABLE [dbo].[ContractInfoHistory] NOCHECK CONSTRAINT [FK_ContractInfoHistory_CreateUser]
GO
ALTER TABLE [dbo].[ContractInfoHistory] NOCHECK CONSTRAINT [FK_ContractInfoHistory_Department]
GO
ALTER TABLE [dbo].[ContractInfoHistory] NOCHECK CONSTRAINT [FK_ContractInfoHistory_MainDepartment]
GO
ALTER TABLE [dbo].[ContractInfoHistory] NOCHECK CONSTRAINT [FK_ContractInfoHistory_PriUser]
GO
ALTER TABLE [dbo].[ContractInfoHistory] NOCHECK CONSTRAINT [FK_ContractInfoHistory_ProjectManager]
GO
