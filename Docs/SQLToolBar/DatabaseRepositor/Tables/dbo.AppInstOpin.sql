CREATE TABLE [dbo].[AppInstOpin]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[InstId] [int] NULL,
[NodeId] [int] NULL,
[NodeStrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NULL,
[CreateDatetime] [datetime] NULL,
[Opinion] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[Result] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppInstOpin] ADD CONSTRAINT [PK_AppInstOpin] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppInstOpin] WITH NOCHECK ADD CONSTRAINT [FK_AppInstOpin_AppInstNode] FOREIGN KEY ([NodeId]) REFERENCES [dbo].[AppInstNode] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[AppInstOpin] NOCHECK CONSTRAINT [FK_AppInstOpin_AppInstNode]
GO
