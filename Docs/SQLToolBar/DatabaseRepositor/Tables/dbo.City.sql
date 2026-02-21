CREATE TABLE [dbo].[City]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ProvinceId] [int] NULL,
[Name] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[DisplayName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[IsEnable] [tinyint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[City] ADD CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
