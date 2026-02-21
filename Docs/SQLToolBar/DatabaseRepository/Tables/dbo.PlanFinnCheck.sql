CREATE TABLE [dbo].[PlanFinnCheck]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[PlanFinanceId] [int] NULL,
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
ALTER TABLE [dbo].[PlanFinnCheck] ADD CONSTRAINT [PK_PlanFinnCheck] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
