CREATE TABLE [dbo].[Inquiry]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Inquirer] [tinyint] NULL,
[ProjectName] [int] NULL,
[ProjectNumber] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[Sites] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[Times] [datetime] NULL,
[ContractExecuteBranch] [int] NULL,
[TheWinningConditions] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[recorder] [tinyint] NULL,
[UsefulLife] [datetime] NULL,
[IsDelete] [tinyint] NULL,
[InState] [tinyint] NULL,
[WfState] [tinyint] NULL,
[CreateUserId] [int] NULL,
[ModifyUserId] [int] NULL,
[InquiryType] [int] NULL,
[WfItem] [int] NULL,
[WfCurrNodeName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Zbdw] [int] NULL,
[Zje] [decimal] (28, 6) NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[Inquiry] ADD
CONSTRAINT [FK_Inquiry_Company] FOREIGN KEY ([Zbdw]) REFERENCES [dbo].[Company] ([Id])
GO
ALTER TABLE [dbo].[Inquiry] ADD CONSTRAINT [PK__Inquiry__3214EC07C13F5034] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Inquiry] ADD CONSTRAINT [FK_Inquiry_Department] FOREIGN KEY ([ContractExecuteBranch]) REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Inquiry] ADD CONSTRAINT [FK_Inquiry_UserInfor] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id])
GO
ALTER TABLE [dbo].[Inquiry] ADD CONSTRAINT [FK_Inquiry_Inquiry] FOREIGN KEY ([ProjectName]) REFERENCES [dbo].[ProjectManager] ([Id])
GO
