CREATE TABLE [dbo].[TempNodeAreaHist]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[StrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[TempHistId] [int] NULL,
[TempId] [int] NULL,
[Name] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Left] [int] NULL,
[Top] [int] NULL,
[Width] [int] NULL,
[Height] [int] NULL,
[Color] [int] NULL,
[Alt] [tinyint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TempNodeAreaHist] ADD CONSTRAINT [PK_TempNodeAreaHist] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
