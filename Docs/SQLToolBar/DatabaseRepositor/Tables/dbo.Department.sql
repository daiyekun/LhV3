CREATE TABLE [dbo].[Department]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Pid] [int] NOT NULL,
[Name] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[No] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CategoryId] [int] NULL,
[DSort] [int] NULL,
[Remark] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[IsMain] [tinyint] NULL,
[ShortName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[IsSubCompany] [int] NULL,
[IsDelete] [tinyint] NULL,
[DStatus] [int] NULL,
[DPath] [nvarchar] (3000) COLLATE Chinese_PRC_CI_AS NULL,
[Leaf] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Department] ADD CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Department] ADD CONSTRAINT [FK_Department_DataDictionary1] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[DataDictionary] ([Id])
GO
