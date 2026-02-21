CREATE TABLE [dbo].[WinningQue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[QueId] [int] NOT NULL,
[WinningName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[WinningUntiId] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[WinningModel] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[WinningTotalPrice] [decimal] (28, 6) NOT NULL,
[WinningUitprice] [decimal] (28, 6) NOT NULL,
[WinningQuantity] [decimal] (28, 6) NOT NULL,
[IsDelete] [int] NOT NULL,
[ModifyUserId] [int] NULL,
[SessionCurrUserId] [int] NULL,
[GuidFileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[SourceFileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[WinningQue] ADD CONSTRAINT [PK_WinningQue] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[WinningQue] WITH NOCHECK ADD CONSTRAINT [FK_WinningQue_Questioning] FOREIGN KEY ([QueId]) REFERENCES [dbo].[Questioning] ([Id])
GO
ALTER TABLE [dbo].[WinningQue] NOCHECK CONSTRAINT [FK_WinningQue_Questioning]
GO
