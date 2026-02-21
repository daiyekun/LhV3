CREATE TABLE [dbo].[BcAttachment]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[FolderName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Path] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NOT NULL,
[GuidFileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[FileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[Name] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NOT NULL,
[CategoryId] [int] NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[DownloadTimes] [int] NULL,
[BcId] [int] NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BcAttachment] ADD CONSTRAINT [PK_BcAttachment] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BcAttachment] WITH NOCHECK ADD CONSTRAINT [FK_BcAttachment_DataDictionary] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[BcAttachment] WITH NOCHECK ADD CONSTRAINT [FK_BcAttachment_UserInfor] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[BcAttachment] WITH NOCHECK ADD CONSTRAINT [FK_BcAttachment_UserInfor1] FOREIGN KEY ([ModifyUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[BcAttachment] NOCHECK CONSTRAINT [FK_BcAttachment_DataDictionary]
GO
ALTER TABLE [dbo].[BcAttachment] NOCHECK CONSTRAINT [FK_BcAttachment_UserInfor]
GO
ALTER TABLE [dbo].[BcAttachment] NOCHECK CONSTRAINT [FK_BcAttachment_UserInfor1]
GO
