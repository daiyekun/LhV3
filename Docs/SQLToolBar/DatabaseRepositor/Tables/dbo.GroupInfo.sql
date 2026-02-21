CREATE TABLE [dbo].[GroupInfo]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[Gstate] [tinyint] NOT NULL,
[IsDelete] [tinyint] NOT NULL,
[CreateUserId] [int] NULL,
[CreateDateTime] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GroupInfo] ADD CONSTRAINT [PK_GroupInfo] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
