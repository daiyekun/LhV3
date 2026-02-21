CREATE TABLE [dbo].[OpenBid]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[unit] [int] NULL,
[TotalPrices] [decimal] (28, 6) NULL,
[UnitPrice] [decimal] (28, 6) NULL,
[personnel] [int] NULL,
[QuesId] [int] NULL,
[IsDelete] [int] NULL,
[OpenId] [int] NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[OpenBid] WITH NOCHECK ADD
CONSTRAINT [FK_OpenBid_OpenBid] FOREIGN KEY ([QuesId]) REFERENCES [dbo].[Questioning] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[OpenBid] NOCHECK CONSTRAINT [FK_OpenBid_OpenBid]
ALTER TABLE [dbo].[OpenBid] WITH NOCHECK ADD
CONSTRAINT [FK_OpenBid_Company] FOREIGN KEY ([unit]) REFERENCES [dbo].[Company] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[OpenBid] NOCHECK CONSTRAINT [FK_OpenBid_Company]
GO
ALTER TABLE [dbo].[OpenBid] ADD CONSTRAINT [PK__OpenBid__3214EC0767D3A879] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
