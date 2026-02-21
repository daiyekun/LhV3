CREATE TABLE [dbo].[BusinessCategory]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Pid] [int] NULL,
[Name] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Code] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[IsDelete] [tinyint] NULL,
[SubCompId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BusinessCategory] ADD CONSTRAINT [PK_BusinessCategory] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
