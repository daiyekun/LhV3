CREATE TABLE [dbo].[ContPlanFinanceHistory]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[PlanFinanceId] [int] NULL,
[ContId] [int] NULL,
[ContHisId] [int] NULL,
[Name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Ftype] [tinyint] NULL,
[AmountMoney] [decimal] (28, 6) NULL,
[SettlementModes] [int] NULL,
[PlanCompleteDateTime] [datetime] NULL,
[Fstate] [tinyint] NULL,
[ProgressState] [tinyint] NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NULL,
[CurrencyId] [int] NULL,
[CurrencyRate] [decimal] (19, 8) NULL,
[SurplusAmount] [decimal] (28, 6) NULL,
[ActAmountMoney] [decimal] (28, 6) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContPlanFinanceHistory] ADD CONSTRAINT [PK_ContPlanFinanceHistory] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContPlanFinanceHistory] WITH NOCHECK ADD CONSTRAINT [FK_ContPlanFinanceHistory_ContractInfoHistory] FOREIGN KEY ([ContHisId]) REFERENCES [dbo].[ContractInfoHistory] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContPlanFinanceHistory] NOCHECK CONSTRAINT [FK_ContPlanFinanceHistory_ContractInfoHistory]
GO
