CREATE TABLE [dbo].[ContTxtTempAndVarStoreRela]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TempHistId] [int] NULL,
[VarId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContTxtTempAndVarStoreRela] ADD CONSTRAINT [PK_ContTxtTempAndVarStoreRela] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
