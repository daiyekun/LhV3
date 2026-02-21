CREATE TABLE [dbo].[SysFunction]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Fcode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Remark] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[IsDelete] [tinyint] NULL,
[ModeId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysFunction] ADD CONSTRAINT [PK_SysFunction] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysFunction] WITH NOCHECK ADD CONSTRAINT [FK_SysFunction_SysModel] FOREIGN KEY ([ModeId]) REFERENCES [dbo].[SysModel] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[SysFunction] NOCHECK CONSTRAINT [FK_SysFunction_SysModel]
GO
