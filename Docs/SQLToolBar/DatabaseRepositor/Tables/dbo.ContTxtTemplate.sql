CREATE TABLE [dbo].[ContTxtTemplate]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
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
ALTER TABLE [dbo].[ContTxtTemplate] ADD CONSTRAINT [PK_ContTxtTemplate] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContTxtTemplate] WITH NOCHECK ADD CONSTRAINT [FK_ContTxtTemplate_UserInfor] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContTxtTemplate] WITH NOCHECK ADD CONSTRAINT [FK_ContTxtTemplate_DataDictionary] FOREIGN KEY ([TepType]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContTxtTemplate] WITH NOCHECK ADD CONSTRAINT [FK_ContTxtTemplate_DataDictionary1] FOREIGN KEY ([TextType]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContTxtTemplate] NOCHECK CONSTRAINT [FK_ContTxtTemplate_UserInfor]
GO
ALTER TABLE [dbo].[ContTxtTemplate] NOCHECK CONSTRAINT [FK_ContTxtTemplate_DataDictionary]
GO
ALTER TABLE [dbo].[ContTxtTemplate] NOCHECK CONSTRAINT [FK_ContTxtTemplate_DataDictionary1]
GO
