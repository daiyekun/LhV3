CREATE TABLE [dbo].[AppInst]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TempHistId] [int] NULL,
[TempId] [int] NULL,
[Version] [int] NULL,
[ObjType] [int] NOT NULL,
[AppObjId] [int] NOT NULL,
[AppObjName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NOT NULL,
[AppObjNo] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[AppObjAmount] [decimal] (28, 6) NULL,
[AppObjCateId] [int] NULL,
[AppState] [int] NOT NULL,
[Mission] [int] NULL,
[StartUserId] [int] NULL,
[StartDateTime] [datetime] NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[CurrentNodeId] [int] NULL,
[CurrentNodeStrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CurrentNodeName] [nvarchar] (150) COLLATE Chinese_PRC_CI_AS NULL,
[CompleteDateTime] [datetime] NULL,
[NewInstId] [int] NULL,
[FinceType] [tinyint] NULL,
[AppSecObjId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppInst] ADD CONSTRAINT [PK_AppInst] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
