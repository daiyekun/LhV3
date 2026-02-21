CREATE TABLE [dbo].[Inquirer]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[InqId] [int] NULL,
[Name] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[position] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[Department] [int] NULL,
[InquiryId] [int] NULL,
[IsDelete] [int] NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[Inquirer] WITH NOCHECK ADD
CONSTRAINT [FK_Inquirer_Inquiry] FOREIGN KEY ([InquiryId]) REFERENCES [dbo].[Inquiry] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[Inquirer] NOCHECK CONSTRAINT [FK_Inquirer_Inquiry]
GO
ALTER TABLE [dbo].[Inquirer] ADD CONSTRAINT [PK__Inquirer__3214EC07A15F82EA] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
