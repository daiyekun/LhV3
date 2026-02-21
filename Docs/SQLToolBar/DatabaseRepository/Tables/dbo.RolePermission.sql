CREATE TABLE [dbo].[RolePermission]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[RoleId] [int] NOT NULL,
[FuncId] [int] NULL,
[FuncCode] [nvarchar] (150) COLLATE Chinese_PRC_CI_AS NULL,
[FuncType] [tinyint] NULL,
[DeptIds] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[ModeId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RolePermission] ADD CONSTRAINT [PK_RolePermission] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
