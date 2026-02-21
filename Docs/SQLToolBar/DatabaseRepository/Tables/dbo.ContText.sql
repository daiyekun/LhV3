CREATE TABLE [dbo].[ContText]
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
ALTER TABLE [dbo].[ContText] WITH NOCHECK ADD
CONSTRAINT [FK_ContText_ContTxtTemplateHist] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[ContTxtTemplateHist] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[ContText] NOCHECK CONSTRAINT [FK_ContText_ContTxtTemplateHist]
GO
ALTER TABLE [dbo].[ContText] ADD CONSTRAINT [PK_ContText] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContText] WITH NOCHECK ADD CONSTRAINT [FK_ContText_ContractInfo] FOREIGN KEY ([ContId]) REFERENCES [dbo].[ContractInfo] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContText] NOCHECK CONSTRAINT [FK_ContText_ContractInfo]
GO
