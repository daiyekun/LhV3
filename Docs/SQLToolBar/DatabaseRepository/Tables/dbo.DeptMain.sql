CREATE TABLE [dbo].[DeptMain]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[DeptId] [int] NULL,
[LawPerson] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[TaxId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[BankName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Account] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Address] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[ZipCode] [nvarchar] (16) COLLATE Chinese_PRC_CI_AS NULL,
[Fax] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[TelePhone] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[InvoiceName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[IsDelete] [tinyint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DeptMain] ADD CONSTRAINT [PK_DeptMain] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DeptMain] WITH NOCHECK ADD CONSTRAINT [FK_DeptMain_Department] FOREIGN KEY ([DeptId]) REFERENCES [dbo].[Department] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[DeptMain] NOCHECK CONSTRAINT [FK_DeptMain_Department]
GO
