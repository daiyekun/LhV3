CREATE TABLE [dbo].[ContTextArchiveItem]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ArchiveId] [int] NULL,
[ContTextId] [int] NULL,
[ArcNumber] [int] NULL,
[ArcRemark] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[ArcCode] [nvarchar] (15) COLLATE Chinese_PRC_CI_AS NULL,
[ArcCabCode] [nvarchar] (15) COLLATE Chinese_PRC_CI_AS NULL,
[SubUser] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[SubDateTime] [datetime] NULL,
[Path] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[FileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[FolderName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[GuidFileName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContTextArchiveItem] ADD CONSTRAINT [PK_ContTextArchiveItem] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
