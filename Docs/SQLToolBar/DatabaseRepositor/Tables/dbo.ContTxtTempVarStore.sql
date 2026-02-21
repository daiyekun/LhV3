CREATE TABLE [dbo].[ContTxtTempVarStore]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Code] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[IsCustomer] [tinyint] NULL,
[Isdelete] [tinyint] NULL,
[TempHistId] [int] NULL,
[StoreType] [int] NULL,
[OriginalID] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContTxtTempVarStore] ADD CONSTRAINT [PK_ContTxtTempVarStore] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
