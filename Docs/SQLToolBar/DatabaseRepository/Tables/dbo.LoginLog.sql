CREATE TABLE [dbo].[LoginLog]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[LoginUserId] [int] NULL,
[RequestNetIp] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[LoginIp] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[Result] [tinyint] NULL,
[CreateDatetime] [datetime] NULL,
[Status] [tinyint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LoginLog] ADD CONSTRAINT [PK_LoginLog] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LoginLog] WITH NOCHECK ADD CONSTRAINT [FK_LoginLog_UserInfor] FOREIGN KEY ([LoginUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[LoginLog] NOCHECK CONSTRAINT [FK_LoginLog_UserInfor]
GO
