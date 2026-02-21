CREATE TABLE [dbo].[AppInstNodeAreaHist]
(
[Id] [int] NOT NULL,
[StrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[InstHistId] [int] NULL,
[Name] [nvarchar] (150) COLLATE Chinese_PRC_CI_AS NULL,
[Left] [int] NULL,
[Top] [int] NULL,
[Width] [int] NULL,
[height] [int] NULL,
[Color] [int] NULL,
[Alt] [tinyint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppInstNodeAreaHist] ADD CONSTRAINT [PK_AppInstNodeAreaHist] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
