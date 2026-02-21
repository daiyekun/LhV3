CREATE TABLE [dbo].[CurrencyManager]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ShortName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Abbreviation] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[Code] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL,
[Rate] [decimal] (18, 8) NULL,
[Remark] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CurrencyManager] ADD CONSTRAINT [PK_CurrencyManager] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
