CREATE TABLE [dbo].[ProjSchedule]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ProjectId] [int] NULL,
[Pitem] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[PlanBeginDateTime] [datetime] NULL,
[PlanCompleteDateTime] [datetime] NULL,
[ActualBeginDateTime] [datetime] NULL,
[ActualCompleteDateTime] [datetime] NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProjSchedule] ADD CONSTRAINT [PK_ProjSchedule] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProjSchedule] WITH NOCHECK ADD CONSTRAINT [FK_ProjSchedule_UserInfor1] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ProjSchedule] NOCHECK CONSTRAINT [FK_ProjSchedule_UserInfor1]
GO
