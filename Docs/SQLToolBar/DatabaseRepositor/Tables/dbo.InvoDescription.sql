CREATE TABLE [dbo].[InvoDescription]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ContInvoId] [int] NULL,
[Name] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Price] [decimal] (28, 6) NULL,
[Count] [int] NULL,
[Total] [decimal] (28, 6) NULL,
[IsDelete] [tinyint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[InvoDescription] ADD CONSTRAINT [PK_InvoDescription] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
