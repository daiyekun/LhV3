CREATE TABLE [dbo].[Interviewpeople]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Inquirer] [int] NULL,
[Name] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[position] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[Department] [int] NULL,
[QuesId] [int] NULL,
[IsDelete] [int] NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[Interviewpeople] WITH NOCHECK ADD
CONSTRAINT [FK_Interviewpeople_Questioning] FOREIGN KEY ([QuesId]) REFERENCES [dbo].[Questioning] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[Interviewpeople] NOCHECK CONSTRAINT [FK_Interviewpeople_Questioning]
GO
ALTER TABLE [dbo].[Interviewpeople] ADD CONSTRAINT [PK__Intervie__3214EC07C6976954] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
