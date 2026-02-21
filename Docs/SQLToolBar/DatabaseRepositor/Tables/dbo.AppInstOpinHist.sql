CREATE TABLE [dbo].[AppInstOpinHist]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[InstHistId] [int] NULL,
[NodeHistId] [int] NULL,
[NodeStrId] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NULL,
[CreateDatetime] [datetime] NULL,
[Opinion] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[Result] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppInstOpinHist] ADD CONSTRAINT [PK_AppInstOpinHist] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
