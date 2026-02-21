CREATE TABLE [dbo].[Remind]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Item] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Name] [nvarchar] (150) COLLATE Chinese_PRC_CI_AS NULL,
[CustomName] [nvarchar] (150) COLLATE Chinese_PRC_CI_AS NULL,
[AheadDays] [int] NULL,
[DelayDays] [int] NULL,
[IsDelete] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Remind] ADD CONSTRAINT [PK_Remind] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
