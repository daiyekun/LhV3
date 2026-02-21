CREATE TABLE [dbo].[CompAttachment]
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
[CompanyId] [int] NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompAttachment] ADD CONSTRAINT [PK_CompAttachment] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_CompAttachment] ON [dbo].[CompAttachment] ([CompanyId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompAttachment] WITH NOCHECK ADD CONSTRAINT [FK_CompAttachment_DataDictionary] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[CompAttachment] WITH NOCHECK ADD CONSTRAINT [FK_CompAttachment_UserInfor1] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[CompAttachment] NOCHECK CONSTRAINT [FK_CompAttachment_DataDictionary]
GO
ALTER TABLE [dbo].[CompAttachment] NOCHECK CONSTRAINT [FK_CompAttachment_UserInfor1]
GO
