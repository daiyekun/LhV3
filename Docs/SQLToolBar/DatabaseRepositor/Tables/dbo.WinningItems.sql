CREATE TABLE [dbo].[WinningItems]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TenderId] [int] NOT NULL,
[WinningName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[WinningUntiId] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[WinningTotalPrice] [decimal] (28, 0) NOT NULL,
[WinningUitprice] [decimal] (28, 0) NOT NULL,
[WinningQuantity] [decimal] (28, 6) NOT NULL,
[WinningModel] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[IsDelete] [int] NOT NULL,
[SessionCurrUserId] [int] NULL,
[ModifyUserId] [int] NULL,
[GuidFileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[SourceFileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[WinningItems] ADD CONSTRAINT [PK_WinningItems] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
