CREATE TABLE [dbo].[QuestioningAttachment]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Path] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[FileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[Name] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[CategoryId] [int] NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[DownloadTimes] [int] NULL,
[ContId] [int] NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL,
[FolderName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[GuidFileName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[QuestioningAttachment] ADD CONSTRAINT [PK_questioningAttachment] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[QuestioningAttachment] WITH NOCHECK ADD CONSTRAINT [FK_questioningAttachment_Questioning] FOREIGN KEY ([ContId]) REFERENCES [dbo].[Questioning] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[QuestioningAttachment] NOCHECK CONSTRAINT [FK_questioningAttachment_Questioning]
GO
