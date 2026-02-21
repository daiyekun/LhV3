CREATE TABLE [dbo].[ScheduleDetail]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ScheduleName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ScheduleSer] [int] NOT NULL,
[Description] [text] COLLATE Chinese_PRC_CI_AS NULL,
[PDescription] [text] COLLATE Chinese_PRC_CI_AS NULL,
[PDDateTime] [datetime] NULL,
[Wancheng] [int] NULL,
[State] [tinyint] NULL,
[CreateUserId] [int] NULL,
[CreateDateTime] [datetime] NULL,
[ModifyUserId] [int] NULL,
[ModifyDateTime] [datetime] NULL,
[IsDelete] [tinyint] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ScheduleDetail] ADD CONSTRAINT [PK_ScheduleDetail] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ScheduleDetail] WITH NOCHECK ADD CONSTRAINT [FK_ScheduleDetail_ScheduleManagement] FOREIGN KEY ([ScheduleSer]) REFERENCES [dbo].[ScheduleManagement] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ScheduleDetail] NOCHECK CONSTRAINT [FK_ScheduleDetail_ScheduleManagement]
GO
