CREATE TABLE [dbo].[ScheduleList]
(
[Id] [int] NOT NULL IDENTITY(1, 1) NOT FOR REPLICATION,
[ScheduleName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ScheduleAttribution] [int] NOT NULL,
[ScheduleDuixiang] [int] NULL,
[Description] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[Descriptionms] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[Tixing] [int] NOT NULL,
[Designee] [int] NOT NULL,
[Stalker] [int] NOT NULL,
[CreateUserId] [int] NULL,
[CreateDateTime] [datetime] NULL,
[ModifyUserId] [int] NULL,
[ModifyDateTime] [datetime] NULL,
[ScheduleId] [int] NULL,
[IsDelete] [tinyint] NULL,
[Myjdtime] [datetime] NULL,
[Mystate] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ScheduleList] ADD CONSTRAINT [PK_ScheduleList] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ScheduleList] WITH NOCHECK ADD CONSTRAINT [FK_ScheduleList_ScheduleManagement] FOREIGN KEY ([ScheduleId]) REFERENCES [dbo].[ScheduleManagement] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ScheduleList] NOCHECK CONSTRAINT [FK_ScheduleList_ScheduleManagement]
GO
