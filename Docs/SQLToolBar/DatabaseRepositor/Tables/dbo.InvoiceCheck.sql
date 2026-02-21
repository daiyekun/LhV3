CREATE TABLE [dbo].[InvoiceCheck]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[InvoiceId] [int] NULL,
[ActualFinanceId] [int] NULL,
[AmountMoney] [decimal] (28, 6) NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[ConfirmUserId] [int] NULL,
[ConfirmDateTIme] [datetime] NULL,
[IsDelete] [tinyint] NULL,
[SettlementDate] [datetime] NULL,
[ChkState] [tinyint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[InvoiceCheck] ADD CONSTRAINT [PK_InvoiceCheck] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_InvoiceCheck] ON [dbo].[InvoiceCheck] ([ActualFinanceId]) ON [PRIMARY]
GO
