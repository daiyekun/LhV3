CREATE TABLE [dbo].[ScheduleManagement]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ScheduleName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ScheduleSer] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Priority] [int] NOT NULL,
[ScheduleAttribution] [int] NOT NULL,
[ScheduleDuixiang] [int] NULL,
[Description] [text] COLLATE Chinese_PRC_CI_AS NULL,
[Designee] [int] NOT NULL,
[Stalker] [int] NOT NULL,
[JhCreateDateTime] [datetime] NULL,
[JhCompleteDateTime] [datetime] NULL,
[SjCreateDateTime] [datetime] NULL,
[SjCompleteDateTime] [datetime] NULL,
[State] [tinyint] NULL,
[Wancheng] [int] NULL,
[CreateUserId] [int] NULL,
[CreateDateTime] [datetime] NULL,
[ModifyUserId] [int] NULL,
[ModifyDateTime] [datetime] NULL,
[IsDelete] [tinyint] NULL,
[Myjdtime] [datetime] NULL,
[Mystate] [int] NULL,
[Gzdatetime] [datetime] NULL,
[Gzstate] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ScheduleManagement] ADD CONSTRAINT [PK_ScheduleManagement] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
