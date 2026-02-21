CREATE TABLE [dbo].[ContTxtTempAndSubField]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[SubFieldId] [int] NULL,
[TempHistId] [int] NULL,
[Sval] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[IsTotal] [tinyint] NULL,
[FieldType] [int] NULL,
[BcId] [int] NULL,
[SortFd] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContTxtTempAndSubField] ADD CONSTRAINT [PK_ContTxtTempAndSubField] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
