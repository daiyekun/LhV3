CREATE TABLE [dbo].[ScheduleDetailAttachment]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Path] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[FileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[Name] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[CategoryId] [int] NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[DownloadTimes] [int] NULL,
[ScheduledId] [int] NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL,
[FolderName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[GuidFileName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ScheduleDetailAttachment] ADD CONSTRAINT [PK_ScheduleDetailAttachment] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ScheduleDetailAttachment] WITH NOCHECK ADD CONSTRAINT [FK_ScheduleDetailAttachment_ScheduleDetail] FOREIGN KEY ([ScheduledId]) REFERENCES [dbo].[ScheduleDetail] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ScheduleDetailAttachment] NOCHECK CONSTRAINT [FK_ScheduleDetailAttachment_ScheduleDetail]
GO
