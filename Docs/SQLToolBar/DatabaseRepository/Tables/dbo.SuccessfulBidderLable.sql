CREATE TABLE [dbo].[SuccessfulBidderLable]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TenderId] [int] NOT NULL,
[SuccessName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
[SuccessUntiId] [int] NULL,
[SuccTotalPrice] [decimal] (28, 6) NOT NULL,
[SuccUitprice] [decimal] (28, 6) NOT NULL,
[SuccId] [int] NULL,
[IS_DELETE] [int] NULL,
[Zbdwid] [int] NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[SuccessfulBidderLable] WITH NOCHECK ADD
CONSTRAINT [FK_SuccessfulBidderLable_TenderInfor] FOREIGN KEY ([SuccessUntiId]) REFERENCES [dbo].[OpeningSituationInfor] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[SuccessfulBidderLable] NOCHECK CONSTRAINT [FK_SuccessfulBidderLable_TenderInfor]
ALTER TABLE [dbo].[SuccessfulBidderLable] WITH NOCHECK ADD
CONSTRAINT [FK_SuccessfulBidderLable_TenderInfor1] FOREIGN KEY ([TenderId]) REFERENCES [dbo].[TenderInfor] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[SuccessfulBidderLable] NOCHECK CONSTRAINT [FK_SuccessfulBidderLable_TenderInfor1]
GO
ALTER TABLE [dbo].[SuccessfulBidderLable] ADD CONSTRAINT [PK_SuccessfulBidderLable] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
