CREATE TABLE [dbo].[ContActualFinance]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ContId] [int] NULL,
[SettlementMethod] [int] NULL,
[FinceType] [tinyint] NULL,
[AmountMoney] [decimal] (28, 6) NULL,
[CurrencyId] [int] NULL,
[CurrencyRate] [decimal] (19, 8) NULL,
[ActualSettlementDate] [datetime] NULL,
[VoucherNo] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Astate] [tinyint] NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[ConfirmUserId] [int] NULL,
[ConfirmDateTIme] [datetime] NULL,
[IsDelete] [tinyint] NULL,
[Reserve1] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Reserve2] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Bank] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Account] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[WfState] [tinyint] NULL,
[WfItem] [int] NULL,
[WfCurrNodeName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContActualFinance] ADD CONSTRAINT [PK_ContActualFinance] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContActualFinance] WITH NOCHECK ADD CONSTRAINT [FK_ContActualFinance_UserInfor1] FOREIGN KEY ([ConfirmUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContActualFinance] WITH NOCHECK ADD CONSTRAINT [FK_ContActualFinance_ContractInfo] FOREIGN KEY ([ContId]) REFERENCES [dbo].[ContractInfo] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContActualFinance] WITH NOCHECK ADD CONSTRAINT [FK_ContActualFinance_UserInfor] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContActualFinance] NOCHECK CONSTRAINT [FK_ContActualFinance_UserInfor1]
GO
ALTER TABLE [dbo].[ContActualFinance] NOCHECK CONSTRAINT [FK_ContActualFinance_ContractInfo]
GO
ALTER TABLE [dbo].[ContActualFinance] NOCHECK CONSTRAINT [FK_ContActualFinance_UserInfor]
GO
