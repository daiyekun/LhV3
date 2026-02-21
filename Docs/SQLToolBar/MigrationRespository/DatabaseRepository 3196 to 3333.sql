/*
This is the script that will be used to migrate the database from revision 3196 to revision 3333.

You can customize the script, and your edits will be used in deployment.
The following objects will be affected:
  dbo.ContTxtTempAndSubField, dbo.ContTxtTempAndVarStoreRela,
  dbo.ContTxtTemplateHist, dbo.ContTxtTempVarStore, dbo.ContTxtTemplate
*/

SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
PRINT N'Creating [dbo].[ContTxtTemplateHist]'
GO
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
[SubcompId] [int] NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK_ContTxtTemplateHist] on [dbo].[ContTxtTemplateHist]'
GO
ALTER TABLE [dbo].[ContTxtTemplateHist] ADD CONSTRAINT [PK_ContTxtTemplateHist] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[ContTxtTemplate]'
GO
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
[SubcompId] [int] NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK_ContTxtTemplate] on [dbo].[ContTxtTemplate]'
GO
ALTER TABLE [dbo].[ContTxtTemplate] ADD CONSTRAINT [PK_ContTxtTemplate] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[ContTxtTempAndSubField]'
GO
CREATE TABLE [dbo].[ContTxtTempAndSubField]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[SubFieldId] [int] NULL,
[TempHistId] [int] NULL,
[Sval] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[IsTotal] [tinyint] NULL,
[FieldType] [int] NULL,
[BcId] [int] NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK_ContTxtTempAndSubField] on [dbo].[ContTxtTempAndSubField]'
GO
ALTER TABLE [dbo].[ContTxtTempAndSubField] ADD CONSTRAINT [PK_ContTxtTempAndSubField] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[ContTxtTempAndVarStoreRela]'
GO
CREATE TABLE [dbo].[ContTxtTempAndVarStoreRela]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TempHistId] [int] NULL,
[VarId] [int] NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK_ContTxtTempAndVarStoreRela] on [dbo].[ContTxtTempAndVarStoreRela]'
GO
ALTER TABLE [dbo].[ContTxtTempAndVarStoreRela] ADD CONSTRAINT [PK_ContTxtTempAndVarStoreRela] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[ContTxtTempVarStore]'
GO
CREATE TABLE [dbo].[ContTxtTempVarStore]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Code] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[IsCustomer] [tinyint] NULL,
[Isdelete] [tinyint] NULL,
[TempHistId] [int] NULL,
[StoreType] [int] NULL,
[OriginalID] [int] NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK_ContTxtTempVarStore] on [dbo].[ContTxtTempVarStore]'
GO
ALTER TABLE [dbo].[ContTxtTempVarStore] ADD CONSTRAINT [PK_ContTxtTempVarStore] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
PRINT N'Adding foreign keys to [dbo].[ContTxtTemplateHist]'
GO
ALTER TABLE [dbo].[ContTxtTemplateHist] WITH NOCHECK  ADD CONSTRAINT [FK_ContTxtTemplateHist_DataDictionary] FOREIGN KEY ([TepType]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[ContTxtTemplateHist] WITH NOCHECK  ADD CONSTRAINT [FK_ContTxtTemplateHist_DataDictionary1] FOREIGN KEY ([TextType]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[ContTxtTemplateHist] WITH NOCHECK  ADD CONSTRAINT [FK_ContTxtTemplateHist_UserInfor] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
PRINT N'Adding foreign keys to [dbo].[ContTxtTemplate]'
GO
ALTER TABLE [dbo].[ContTxtTemplate] WITH NOCHECK  ADD CONSTRAINT [FK_ContTxtTemplate_DataDictionary] FOREIGN KEY ([TepType]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[ContTxtTemplate] WITH NOCHECK  ADD CONSTRAINT [FK_ContTxtTemplate_DataDictionary1] FOREIGN KEY ([TextType]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[ContTxtTemplate] WITH NOCHECK  ADD CONSTRAINT [FK_ContTxtTemplate_UserInfor] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
PRINT N'Disabling constraints on [dbo].[ContTxtTemplateHist]'
GO
ALTER TABLE [dbo].[ContTxtTemplateHist] NOCHECK CONSTRAINT [FK_ContTxtTemplateHist_DataDictionary]
ALTER TABLE [dbo].[ContTxtTemplateHist] NOCHECK CONSTRAINT [FK_ContTxtTemplateHist_DataDictionary1]
ALTER TABLE [dbo].[ContTxtTemplateHist] NOCHECK CONSTRAINT [FK_ContTxtTemplateHist_UserInfor]
GO
PRINT N'Disabling constraints on [dbo].[ContTxtTemplate]'
GO
ALTER TABLE [dbo].[ContTxtTemplate] NOCHECK CONSTRAINT [FK_ContTxtTemplate_DataDictionary]
ALTER TABLE [dbo].[ContTxtTemplate] NOCHECK CONSTRAINT [FK_ContTxtTemplate_DataDictionary1]
ALTER TABLE [dbo].[ContTxtTemplate] NOCHECK CONSTRAINT [FK_ContTxtTemplate_UserInfor]
GO
