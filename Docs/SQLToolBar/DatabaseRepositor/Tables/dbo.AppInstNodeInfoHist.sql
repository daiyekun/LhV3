CREATE TABLE [dbo].[AppInstNodeInfoHist]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[InstHistId] [int] NULL,
[InstNodeHistId] [int] NULL,
[NodeStrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Nrule] [int] NULL,
[ReviseText] [int] NULL,
[GroupId] [int] NULL,
[GroupName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Min] [decimal] (28, 6) NULL,
[Max] [decimal] (28, 6) NULL,
[IsMin] [int] NULL,
[IsMax] [int] NULL,
[NodeState] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppInstNodeInfoHist] ADD CONSTRAINT [PK_AppInstNodeInfoHist] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
