CREATE TABLE [dbo].[ContSubDelivery]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[SubId] [int] NULL,
[PlanDateTime] [datetime] NULL,
[ActualDateTime] [datetime] NULL,
[ThedeliveryAmount] [decimal] (28, 6) NULL,
[Dstate] [int] NULL,
[AmountMoney] [decimal] (28, 6) NULL,
[Num] [int] NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NULL,
[DevState] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContSubDelivery] ADD CONSTRAINT [PK_ContSubDelivery] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContSubDelivery] WITH NOCHECK ADD CONSTRAINT [FK_ContSubDelivery_ContSubjectMatter] FOREIGN KEY ([SubId]) REFERENCES [dbo].[ContSubjectMatter] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContSubDelivery] NOCHECK CONSTRAINT [FK_ContSubDelivery_ContSubjectMatter]
GO
