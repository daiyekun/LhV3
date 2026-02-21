CREATE TABLE [dbo].[AppInstNodeInfo]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[InstId] [int] NULL,
[InstNodeId] [int] NULL,
[NodeStrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Nrule] [int] NULL,
[ReviseText] [int] NULL,
[GroupId] [int] NULL,
[GroupName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Min] [decimal] (28, 6) NULL,
[Max] [decimal] (28, 6) NULL,
[IsMin] [int] NULL,
[IsMax] [int] NULL,
[NodeState] [int] NULL,
[SubmitMsg] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppInstNodeInfo] ADD CONSTRAINT [PK_AppInstNodeInfo] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
