CREATE TABLE [dbo].[UserModule]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NOT NULL,
[ModuleId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserModule] ADD CONSTRAINT [PK_UserModule] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
