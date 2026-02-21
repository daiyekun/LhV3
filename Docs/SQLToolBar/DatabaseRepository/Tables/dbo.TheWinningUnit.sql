CREATE TABLE [dbo].[TheWinningUnit]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[WinningUnit] [int] NULL,
[BidPrices] [decimal] (28, 2) NULL,
[BidPrice] [decimal] (28, 2) NULL,
[BidUser] [int] NULL,
[LnquiryId] [int] NULL,
[IsDelete] [int] NULL,
[Zbdwid] [int] NULL,
[Lxr] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Lxfs] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[TheWinningUnit] WITH NOCHECK ADD
CONSTRAINT [FK_TheWinningUnit_Inquiry] FOREIGN KEY ([LnquiryId]) REFERENCES [dbo].[Inquiry] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[TheWinningUnit] NOCHECK CONSTRAINT [FK_TheWinningUnit_Inquiry]
ALTER TABLE [dbo].[TheWinningUnit] WITH NOCHECK ADD
CONSTRAINT [FK_TheWinningUnit_OpenTenderCondition] FOREIGN KEY ([WinningUnit]) REFERENCES [dbo].[OpenTenderCondition] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[TheWinningUnit] NOCHECK CONSTRAINT [FK_TheWinningUnit_OpenTenderCondition]
GO
ALTER TABLE [dbo].[TheWinningUnit] ADD CONSTRAINT [PK__TheWinni__3214EC07CA741D73] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
