CREATE TABLE [dbo].[ContDescription]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ContId] [int] NULL,
[Citem] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[Ccontent] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContDescription] ADD CONSTRAINT [PK_ContDescription] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
