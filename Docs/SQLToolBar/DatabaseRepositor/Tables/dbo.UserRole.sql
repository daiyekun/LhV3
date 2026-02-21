CREATE TABLE [dbo].[UserRole]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NULL,
[RoleId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
