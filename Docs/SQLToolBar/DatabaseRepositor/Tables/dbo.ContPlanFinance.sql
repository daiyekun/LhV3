CREATE TABLE [dbo].[ContPlanFinance]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ContId] [int] NULL,
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
[IsDelete] [tinyint] NOT NULL,
[CurrencyId] [int] NULL,
[CurrencyRate] [decimal] (19, 8) NULL,
[SubAmount] [decimal] (28, 6) NULL,
[ConfirmedAmount] [decimal] (28, 6) NULL,
[ActSettlementDate] [datetime] NULL,
[SurplusAmount] [decimal] (28, 6) NULL,
[ActAmountMoney] [decimal] (28, 6) NULL,
[CheckAmount] [decimal] (28, 6) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContPlanFinance] ADD CONSTRAINT [PK_ContPlanFinance] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContPlanFinance] WITH NOCHECK ADD CONSTRAINT [FK_ContPlanFinance_ContractInfo1] FOREIGN KEY ([ContId]) REFERENCES [dbo].[ContractInfo] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContPlanFinance] NOCHECK CONSTRAINT [FK_ContPlanFinance_ContractInfo1]
GO
