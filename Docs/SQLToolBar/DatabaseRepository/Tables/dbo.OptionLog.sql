CREATE TABLE [dbo].[OptionLog]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NULL,
[ControllerName] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[ActionName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (max) COLLATE Chinese_PRC_CI_AS NULL,
[RequestUrl] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[RequestMethod] [tinyint] NULL,
[RequestData] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[RequestIp] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[RequestNetIp] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[TotalTime] [float] NULL,
[CreateDatetime] [datetime] NULL,
[Status] [tinyint] NULL,
[ActionTitle] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[ExecResult] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[OptionType] [tinyint] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[OptionLog] ADD CONSTRAINT [PK_OptionLog] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OptionLog] WITH NOCHECK ADD CONSTRAINT [FK_OptionLog_UserInfor] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[OptionLog] NOCHECK CONSTRAINT [FK_OptionLog_UserInfor]
GO
