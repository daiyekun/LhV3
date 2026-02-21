CREATE TABLE [dbo].[SealManager]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[MainDeptId] [int] NULL,
[SealName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[SealCode] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[UserId] [int] NULL,
[DeptId] [int] NULL,
[EnabledDate] [datetime] NOT NULL,
[SealState] [tinyint] NOT NULL,
[Remark] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SealManager] ADD CONSTRAINT [PK_SealManager] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
