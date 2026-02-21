CREATE TABLE [dbo].[Bidlabel]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[WinningUnit] [int] NULL,
[BidPrices] [decimal] (28, 2) NULL,
[BidPrice] [decimal] (28, 2) NULL,
[BidUser] [int] NULL,
[QuesId] [int] NULL,
[Bidlabel] [int] NULL,
[Zbdwid] [int] NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[Bidlabel] WITH NOCHECK ADD
CONSTRAINT [FK_Bidlabel_Questioning] FOREIGN KEY ([QuesId]) REFERENCES [dbo].[Questioning] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[Bidlabel] NOCHECK CONSTRAINT [FK_Bidlabel_Questioning]
ALTER TABLE [dbo].[Bidlabel] WITH NOCHECK ADD
CONSTRAINT [FK_Bidlabel_OpenBid] FOREIGN KEY ([WinningUnit]) REFERENCES [dbo].[OpenBid] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[Bidlabel] NOCHECK CONSTRAINT [FK_Bidlabel_OpenBid]
GO
ALTER TABLE [dbo].[Bidlabel] ADD CONSTRAINT [PK__Bidlabel__3214EC079942C31E] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
