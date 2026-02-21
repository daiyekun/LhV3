CREATE TABLE [dbo].[ActFinceFile]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Path] [nchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[FolderName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[FileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[Name] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[CategoryId] [int] NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[DownloadTimes] [int] NULL,
[FileSize] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[ActId] [int] NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL,
[GuidFileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ActFinceFile] ADD CONSTRAINT [PK_ActFinceFile] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
