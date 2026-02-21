CREATE TABLE [dbo].[RoleModule]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[RoleId] [int] NOT NULL,
[ModuleId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RoleModule] ADD CONSTRAINT [PK_RoleModule] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
