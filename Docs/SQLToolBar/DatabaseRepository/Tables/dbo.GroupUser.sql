CREATE TABLE [dbo].[GroupUser]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NULL,
[GroupId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GroupUser] ADD CONSTRAINT [PK_GroupUser] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
