CREATE TABLE [dbo].[ContInvoice]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ContId] [int] NULL,
[InType] [int] NULL,
[InTitle] [nvarchar] (150) COLLATE Chinese_PRC_CI_AS NULL,
[TaxpayerIdentification] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[InAddress] [nvarchar] (150) COLLATE Chinese_PRC_CI_AS NULL,
[InTel] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[BankName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[BankAccount] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[AmountMoney] [decimal] (28, 6) NULL,
[MakeOutDateTime] [datetime] NULL,
[InCode] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[InState] [tinyint] NULL,
[CurrencyId] [int] NULL,
[CurrencyRate] [decimal] (19, 8) NULL,
[InContent] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[Reserve1] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Reserve2] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[ConfirmUserId] [int] NULL,
[ConfirmDateTIme] [datetime] NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL,
[SubAmount] [decimal] (28, 6) NULL,
[ConfirmedAmount] [decimal] (28, 6) NULL,
[SurplusAmount] [decimal] (28, 6) NULL,
[ActAmountMoney] [decimal] (28, 6) NULL,
[CheckAmount] [decimal] (28, 6) NULL,
[WfState] [tinyint] NULL,
[WfItem] [int] NULL,
[WfCurrNodeName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContInvoice] ADD CONSTRAINT [PK_ContInvoice] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContInvoice] WITH NOCHECK ADD CONSTRAINT [FK_ContInvoice_UserInfor2] FOREIGN KEY ([ConfirmUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContInvoice] WITH NOCHECK ADD CONSTRAINT [FK_ContInvoice_ContractInfo] FOREIGN KEY ([ContId]) REFERENCES [dbo].[ContractInfo] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContInvoice] WITH NOCHECK ADD CONSTRAINT [FK_ContInvoice_UserInfor1] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContInvoice] NOCHECK CONSTRAINT [FK_ContInvoice_UserInfor2]
GO
ALTER TABLE [dbo].[ContInvoice] NOCHECK CONSTRAINT [FK_ContInvoice_ContractInfo]
GO
ALTER TABLE [dbo].[ContInvoice] NOCHECK CONSTRAINT [FK_ContInvoice_UserInfor1]
GO
