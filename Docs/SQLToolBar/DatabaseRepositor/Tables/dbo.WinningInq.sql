CREATE TABLE [dbo].[WinningInq]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Inqid] [int] NULL,
[WinningName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[WinningUntiId] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[WinningModel] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[WinningTotalPrice] [decimal] (28, 6) NOT NULL,
[WinningUitprice] [decimal] (28, 6) NOT NULL,
[WinningQuantity] [decimal] (28, 6) NOT NULL,
[IsDelete] [int] NOT NULL,
[SessionCurrUserId] [int] NULL,
[ModifyUserId] [int] NULL,
[GuidFileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[SourceFileName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[WinningInq] ADD CONSTRAINT [PK_WinningInq] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[WinningInq] WITH NOCHECK ADD CONSTRAINT [FK_WinningInq_Inquiry] FOREIGN KEY ([Inqid]) REFERENCES [dbo].[Inquiry] ([Id])
GO
ALTER TABLE [dbo].[WinningInq] NOCHECK CONSTRAINT [FK_WinningInq_Inquiry]
GO
