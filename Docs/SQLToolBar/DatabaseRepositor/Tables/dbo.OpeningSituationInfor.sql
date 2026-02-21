CREATE TABLE [dbo].[OpeningSituationInfor]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TenderId] [int] NOT NULL,
[OpenSituationName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Unit] [int] NULL,
[TotalPrice] [decimal] (28, 6) NOT NULL,
[Uitprice] [decimal] (28, 6) NOT NULL,
[UserId] [int] NOT NULL,
[IS_DELETE] [int] NOT NULL,
[LnquiryId] [int] NULL,
[OpenId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OpeningSituationInfor] ADD CONSTRAINT [PK_OpeningSituationInfor] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OpeningSituationInfor] WITH NOCHECK ADD CONSTRAINT [FK_OpeningSituationInfor_OpeningSituationInfor] FOREIGN KEY ([LnquiryId]) REFERENCES [dbo].[Inquiry] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[OpeningSituationInfor] WITH NOCHECK ADD CONSTRAINT [FK_OpeningSituationInfor_TenderInfor1] FOREIGN KEY ([TenderId]) REFERENCES [dbo].[TenderInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[OpeningSituationInfor] WITH NOCHECK ADD CONSTRAINT [FK_OpeningSituationInfor_TenderInfor] FOREIGN KEY ([Unit]) REFERENCES [dbo].[Company] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[OpeningSituationInfor] NOCHECK CONSTRAINT [FK_OpeningSituationInfor_OpeningSituationInfor]
GO
ALTER TABLE [dbo].[OpeningSituationInfor] NOCHECK CONSTRAINT [FK_OpeningSituationInfor_TenderInfor1]
GO
ALTER TABLE [dbo].[OpeningSituationInfor] NOCHECK CONSTRAINT [FK_OpeningSituationInfor_TenderInfor]
GO
