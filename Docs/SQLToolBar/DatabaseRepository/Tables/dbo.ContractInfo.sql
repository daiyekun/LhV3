CREATE TABLE [dbo].[ContractInfo]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
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
[ContHid] [int] NULL,
[WfState] [tinyint] NULL,
[WfItem] [int] NULL,
[WfCurrNodeName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Zbid] [int] NULL,
[Xjid] [int] NULL,
[Ytid] [int] NULL,
[ContSingNo] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[ContStaticId] [int] NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[ContractInfo] WITH NOCHECK ADD
CONSTRAINT [FK_ContractInfo_ContStatistics] FOREIGN KEY ([ContStaticId]) REFERENCES [dbo].[ContStatistic] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[ContractInfo] NOCHECK CONSTRAINT [FK_ContractInfo_ContStatistics]


ALTER TABLE [dbo].[ContractInfo] ADD
CONSTRAINT [FK_ContractInfo_Inquiry] FOREIGN KEY ([Xjid]) REFERENCES [dbo].[Inquiry] ([Id])
ALTER TABLE [dbo].[ContractInfo] ADD
CONSTRAINT [FK_ContractInfo_Questioning] FOREIGN KEY ([Ytid]) REFERENCES [dbo].[Questioning] ([Id])
ALTER TABLE [dbo].[ContractInfo] ADD
CONSTRAINT [FK_ContractInfo_TenderInfor] FOREIGN KEY ([Zbid]) REFERENCES [dbo].[TenderInfor] ([Id])
GO
ALTER TABLE [dbo].[ContractInfo] ADD CONSTRAINT [PK_ContractInfo] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContractInfo] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfo_Company] FOREIGN KEY ([CompId]) REFERENCES [dbo].[Company] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfo] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfo_Company3] FOREIGN KEY ([CompId3]) REFERENCES [dbo].[Company] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfo] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfo_Company4] FOREIGN KEY ([CompId4]) REFERENCES [dbo].[Company] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfo] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfo_ContSource] FOREIGN KEY ([ContSourceId]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfo] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfo_ContType] FOREIGN KEY ([ContTypeId]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfo] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfo_CreateUser] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfo] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfo_CurrencyManager] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[CurrencyManager] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfo] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfo_Department] FOREIGN KEY ([DeptId]) REFERENCES [dbo].[Department] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfo] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfo_MainDepartment] FOREIGN KEY ([MainDeptId]) REFERENCES [dbo].[Department] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfo] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfo_PriUser] FOREIGN KEY ([PrincipalUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfo] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfo_ProjectManager] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[ProjectManager] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfo] WITH NOCHECK ADD CONSTRAINT [FK_ContractInfo_SumContractInfo] FOREIGN KEY ([SumContId]) REFERENCES [dbo].[ContractInfo] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractInfo] NOCHECK CONSTRAINT [FK_ContractInfo_Company]
GO
ALTER TABLE [dbo].[ContractInfo] NOCHECK CONSTRAINT [FK_ContractInfo_Company3]
GO
ALTER TABLE [dbo].[ContractInfo] NOCHECK CONSTRAINT [FK_ContractInfo_Company4]
GO
ALTER TABLE [dbo].[ContractInfo] NOCHECK CONSTRAINT [FK_ContractInfo_ContSource]
GO
ALTER TABLE [dbo].[ContractInfo] NOCHECK CONSTRAINT [FK_ContractInfo_ContType]
GO
ALTER TABLE [dbo].[ContractInfo] NOCHECK CONSTRAINT [FK_ContractInfo_CreateUser]
GO
ALTER TABLE [dbo].[ContractInfo] NOCHECK CONSTRAINT [FK_ContractInfo_CurrencyManager]
GO
ALTER TABLE [dbo].[ContractInfo] NOCHECK CONSTRAINT [FK_ContractInfo_Department]
GO
ALTER TABLE [dbo].[ContractInfo] NOCHECK CONSTRAINT [FK_ContractInfo_MainDepartment]
GO
ALTER TABLE [dbo].[ContractInfo] NOCHECK CONSTRAINT [FK_ContractInfo_PriUser]
GO
ALTER TABLE [dbo].[ContractInfo] NOCHECK CONSTRAINT [FK_ContractInfo_ProjectManager]
GO
ALTER TABLE [dbo].[ContractInfo] NOCHECK CONSTRAINT [FK_ContractInfo_SumContractInfo]
GO
