/*
This is the script that will be used to migrate the database from revision 6084 to revision 6111.

You can customize the script, and your edits will be used in deployment.
The following objects will be affected:
  dbo.ContTextHistory, dbo.InvoFile, dbo.ContText
*/

SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
PRINT N'Dropping foreign keys from [dbo].[ContTextHistory]'
GO
ALTER TABLE [dbo].[ContTextHistory] DROP CONSTRAINT[FK_ContTextHistory_ContTxtTemplateHist]
ALTER TABLE [dbo].[ContTextHistory] DROP CONSTRAINT[FK_ContTextHistory_ContractInfoHistory]
GO
PRINT N'Dropping foreign keys from [dbo].[ContTextSeal]'
GO
ALTER TABLE [dbo].[ContTextSeal] DROP CONSTRAINT[FK_ContTextSeal_ContText]
GO
PRINT N'Dropping foreign keys from [dbo].[ContText]'
GO
ALTER TABLE [dbo].[ContText] DROP CONSTRAINT[FK_ContText_ContTxtTemplateHist]
ALTER TABLE [dbo].[ContText] DROP CONSTRAINT[FK_ContText_ContractInfo]
GO
PRINT N'Dropping constraints from [dbo].[ContTextHistory]'
GO
ALTER TABLE [dbo].[ContTextHistory] DROP CONSTRAINT [PK_ContTextHistory]
GO
PRINT N'Dropping constraints from [dbo].[ContText]'
GO
ALTER TABLE [dbo].[ContText] DROP CONSTRAINT [PK_ContText]
GO
PRINT N'Rebuilding [dbo].[ContText]'
GO
CREATE TABLE [dbo].[tmp_rg_xx_ContText]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Path] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[FileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[Name] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[TemplateId] [int] NULL,
[CategoryId] [int] NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[DownloadTimes] [int] NULL,
[ContId] [int] NULL,
[ContHisId] [int] NULL,
[Stage] [int] NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL,
[IsFromTemp] [tinyint] NULL,
[Versions] [int] NULL,
[ElectronicVersionPath] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[Archivedcopies] [int] NULL,
[Borrowedcopies] [int] NULL,
[WordPath] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[ExtenName] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL,
[TextLock] [int] NULL,
[LockTime] [datetime] NULL,
[GuidFileName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[FolderName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tmp_rg_xx_ContText] ON
GO
INSERT INTO [dbo].[tmp_rg_xx_ContText]([Id], [Path], [FileName], [Name], [TemplateId], [CategoryId], [Remark], [DownloadTimes], [ContId], [ContHisId], [Stage], [CreateUserId], [CreateDateTime], [ModifyUserId], [ModifyDateTime], [IsDelete], [IsFromTemp], [Versions], [ElectronicVersionPath], [Archivedcopies], [Borrowedcopies], [WordPath], [ExtenName], [TextLock], [LockTime], [GuidFileName], [FolderName]) SELECT [Id], [Path], [FileName], [Name], [TemplateId], [CategoryId], [Remark], [DownloadTimes], [ContId], [ContHisId], [Stage], [CreateUserId], [CreateDateTime], [ModifyUserId], [ModifyDateTime], [IsDelete], [IsFromTemp], [Versions], [ElectronicVersionPath], [Archivedcopies], [Borrowedcopies], [WordPath], [ExtenName], [TextLock], [LockTime], [GuidFileName], [FolderName] FROM [dbo].[ContText]
GO
SET IDENTITY_INSERT [dbo].[tmp_rg_xx_ContText] OFF
GO
DROP TABLE [dbo].[ContText]
GO
EXEC sp_rename N'[dbo].[tmp_rg_xx_ContText]', N'ContText'
GO
PRINT N'Creating primary key [PK_ContText] on [dbo].[ContText]'
GO
ALTER TABLE [dbo].[ContText] ADD CONSTRAINT [PK_ContText] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
PRINT N'Rebuilding [dbo].[ContTextHistory]'
GO
CREATE TABLE [dbo].[tmp_rg_xx_ContTextHistory]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ContTxtId] [int] NULL,
[Path] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[FileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[Name] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[TemplateId] [int] NULL,
[CategoryId] [int] NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[DownloadTimes] [int] NULL,
[ContId] [int] NULL,
[ContHisId] [int] NULL,
[Stage] [int] NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL,
[IsFromTemp] [tinyint] NULL,
[Versions] [int] NULL,
[ElectronicVersionPath] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[Archivedcopies] [int] NULL,
[Borrowedcopies] [int] NULL,
[WordPath] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[TextLock] [int] NULL,
[LockTime] [datetime] NULL,
[GuidFileName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[FolderName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[ExtenName] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tmp_rg_xx_ContTextHistory] ON
GO
INSERT INTO [dbo].[tmp_rg_xx_ContTextHistory]([Id], [ContTxtId], [Path], [FileName], [Name], [TemplateId], [CategoryId], [Remark], [DownloadTimes], [ContId], [ContHisId], [Stage], [CreateUserId], [CreateDateTime], [ModifyUserId], [ModifyDateTime], [IsDelete], [IsFromTemp], [Versions], [ElectronicVersionPath], [Archivedcopies], [Borrowedcopies], [WordPath], [TextLock], [LockTime], [GuidFileName], [FolderName], [ExtenName]) SELECT [Id], [ContTxtId], [Path], [FileName], [Name], [TemplateId], [CategoryId], [Remark], [DownloadTimes], [ContId], [ContHisId], [Stage], [CreateUserId], [CreateDateTime], [ModifyUserId], [ModifyDateTime], [IsDelete], [IsFromTemp], [Versions], [ElectronicVersionPath], [Archivedcopies], [Borrowedcopies], [WordPath], [TextLock], [LockTime], [GuidFileName], [FolderName], [ExtenName] FROM [dbo].[ContTextHistory]
GO
SET IDENTITY_INSERT [dbo].[tmp_rg_xx_ContTextHistory] OFF
GO
DROP TABLE [dbo].[ContTextHistory]
GO
EXEC sp_rename N'[dbo].[tmp_rg_xx_ContTextHistory]', N'ContTextHistory'
GO
PRINT N'Creating primary key [PK_ContTextHistory] on [dbo].[ContTextHistory]'
GO
ALTER TABLE [dbo].[ContTextHistory] ADD CONSTRAINT [PK_ContTextHistory] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
PRINT N'Altering [dbo].[InvoFile]'
GO
EXEC sp_rename N'[dbo].[InvoFile].[ActId]', N'InvoId', 'COLUMN'
GO
PRINT N'Adding foreign keys to [dbo].[ContTextHistory]'
GO
ALTER TABLE [dbo].[ContTextHistory] WITH NOCHECK  ADD CONSTRAINT [FK_ContTextHistory_ContTxtTemplateHist] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[ContTxtTemplateHist] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[ContTextHistory] WITH NOCHECK  ADD CONSTRAINT [FK_ContTextHistory_ContractInfoHistory] FOREIGN KEY ([ContHisId]) REFERENCES [dbo].[ContractInfoHistory] ([Id]) NOT FOR REPLICATION
GO
PRINT N'Adding foreign keys to [dbo].[ContTextSeal]'
GO
ALTER TABLE [dbo].[ContTextSeal] WITH NOCHECK  ADD CONSTRAINT [FK_ContTextSeal_ContText] FOREIGN KEY ([ContTextId]) REFERENCES [dbo].[ContText] ([Id]) NOT FOR REPLICATION
GO
PRINT N'Adding foreign keys to [dbo].[ContText]'
GO
ALTER TABLE [dbo].[ContText] WITH NOCHECK  ADD CONSTRAINT [FK_ContText_ContTxtTemplateHist] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[ContTxtTemplateHist] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[ContText] WITH NOCHECK  ADD CONSTRAINT [FK_ContText_ContractInfo] FOREIGN KEY ([ContId]) REFERENCES [dbo].[ContractInfo] ([Id]) NOT FOR REPLICATION
GO
PRINT N'Disabling constraints on [dbo].[ContTextHistory]'
GO
ALTER TABLE [dbo].[ContTextHistory] NOCHECK CONSTRAINT [FK_ContTextHistory_ContTxtTemplateHist]
ALTER TABLE [dbo].[ContTextHistory] NOCHECK CONSTRAINT [FK_ContTextHistory_ContractInfoHistory]
GO
PRINT N'Disabling constraints on [dbo].[ContTextSeal]'
GO
ALTER TABLE [dbo].[ContTextSeal] NOCHECK CONSTRAINT [FK_ContTextSeal_ContText]
GO
PRINT N'Disabling constraints on [dbo].[ContText]'
GO
ALTER TABLE [dbo].[ContText] NOCHECK CONSTRAINT [FK_ContText_ContTxtTemplateHist]
ALTER TABLE [dbo].[ContText] NOCHECK CONSTRAINT [FK_ContText_ContractInfo]
GO
