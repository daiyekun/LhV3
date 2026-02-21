CREATE TABLE [dbo].[ContSubjectMatter]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ContId] [int] NULL,
[Name] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Spec] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Stype] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Unit] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[Amount] [decimal] (28, 6) NULL,
[Price] [decimal] (28, 6) NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL,
[BcInstanceId] [int] NULL,
[IsFromCategory] [tinyint] NULL,
[DiscountRate] [decimal] (28, 6) NULL,
[SubTotalRate] [decimal] (28, 6) NULL,
[SubTotal] [decimal] (28, 6) NULL,
[ComplateAmount] [decimal] (28, 6) NULL,
[PlanDateTime] [datetime] NULL,
[SalePrice] [decimal] (28, 6) NULL,
[AmountMoney] [decimal] (28, 6) NULL,
[NominalQuote] [decimal] (28, 6) NULL,
[NominalRate] [decimal] (28, 6) NULL,
[DelNum] [decimal] (28, 6) NULL,
[SjJfRq] [datetime] NULL,
[SubState] [tinyint] NULL,
[Field1] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContSubjectMatter] ADD CONSTRAINT [PK_ContSubjectMatter] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContSubjectMatter] WITH NOCHECK ADD CONSTRAINT [FK_ContSubjectMatter_BcInstance] FOREIGN KEY ([BcInstanceId]) REFERENCES [dbo].[BcInstance] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContSubjectMatter] WITH NOCHECK ADD CONSTRAINT [FK_ContSubjectMatter_ContractInfo] FOREIGN KEY ([ContId]) REFERENCES [dbo].[ContractInfo] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContSubjectMatter] NOCHECK CONSTRAINT [FK_ContSubjectMatter_BcInstance]
GO
ALTER TABLE [dbo].[ContSubjectMatter] NOCHECK CONSTRAINT [FK_ContSubjectMatter_ContractInfo]
GO
