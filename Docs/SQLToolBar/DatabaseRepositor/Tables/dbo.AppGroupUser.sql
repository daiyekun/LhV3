CREATE TABLE [dbo].[AppGroupUser]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[GroupId] [int] NULL,
[UserId] [int] NULL,
[InstId] [int] NULL,
[NodeStrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppGroupUser] ADD CONSTRAINT [PK_AppGroupUser] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
