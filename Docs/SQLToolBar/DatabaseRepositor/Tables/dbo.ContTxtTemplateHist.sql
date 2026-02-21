CREATE TABLE [dbo].[ContTxtTemplateHist]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TempId] [int] NULL,
[Name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Code] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Path] [nvarchar] (300) COLLATE Chinese_PRC_CI_AS NULL,
[TepType] [int] NULL,
[TextType] [int] NULL,
[DeptIds] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NULL,
[CreateDateTime] [datetime] NULL,
[ModifyUserId] [int] NULL,
[ModifyDateTime] [datetime] NULL,
[IsDelete] [tinyint] NULL,
[Vesion] [int] NULL,
[UseVersion] [tinyint] NULL,
[Tstate] [int] NULL,
[FieldType] [int] NULL,
[WordEdit] [tinyint] NULL,
[ShowType] [int] NULL,
[ShowTypeNumber] [int] NULL,
[MingXiTitle] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[SubcompId] [int] NULL,
[ShowSub] [int] NULL,
[TepTypes] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContTxtTemplateHist] ADD CONSTRAINT [PK_ContTxtTemplateHist] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContTxtTemplateHist] WITH NOCHECK ADD CONSTRAINT [FK_ContTxtTemplateHist_UserInfor] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContTxtTemplateHist] WITH NOCHECK ADD CONSTRAINT [FK_ContTxtTemplateHist_DataDictionary] FOREIGN KEY ([TepType]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContTxtTemplateHist] WITH NOCHECK ADD CONSTRAINT [FK_ContTxtTemplateHist_DataDictionary1] FOREIGN KEY ([TextType]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContTxtTemplateHist] NOCHECK CONSTRAINT [FK_ContTxtTemplateHist_UserInfor]
GO
ALTER TABLE [dbo].[ContTxtTemplateHist] NOCHECK CONSTRAINT [FK_ContTxtTemplateHist_DataDictionary]
GO
ALTER TABLE [dbo].[ContTxtTemplateHist] NOCHECK CONSTRAINT [FK_ContTxtTemplateHist_DataDictionary1]
GO
