CREATE TABLE [dbo].[ProjAttachment]
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
[ProjectId] [int] NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL,
[GuidFileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProjAttachment] ADD CONSTRAINT [PK_ProjAttachment] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProjAttachment] WITH NOCHECK ADD CONSTRAINT [FK_ProjAttachment_DataDictionary] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ProjAttachment] WITH NOCHECK ADD CONSTRAINT [FK_ProjAttachment_UserInfor1] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ProjAttachment] NOCHECK CONSTRAINT [FK_ProjAttachment_DataDictionary]
GO
ALTER TABLE [dbo].[ProjAttachment] NOCHECK CONSTRAINT [FK_ProjAttachment_UserInfor1]
GO
