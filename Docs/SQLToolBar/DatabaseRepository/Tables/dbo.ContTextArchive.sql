CREATE TABLE [dbo].[ContTextArchive]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ContTextId] [int] NULL,
[ArcCode] [nvarchar] (15) COLLATE Chinese_PRC_CI_AS NULL,
[ArcCabCode] [nvarchar] (15) COLLATE Chinese_PRC_CI_AS NULL,
[ArcSumNumber] [int] NULL,
[BorrSumNumber] [int] NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContTextArchive] ADD CONSTRAINT [PK_ContTextArchive] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
