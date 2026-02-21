CREATE TABLE [dbo].[OpenTenderCondition]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[unit] [int] NULL,
[TotalPrices] [decimal] (28, 6) NULL,
[UnitPrice] [decimal] (28, 6) NULL,
[personnel] [int] NULL,
[LnquiryId] [int] NULL,
[IsDelete] [int] NULL,
[OpenId] [int] NULL,
[Lxr] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Lxfs] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[OpenTenderCondition] WITH NOCHECK ADD
CONSTRAINT [FK_OpenTenderCondition_Inquiry] FOREIGN KEY ([LnquiryId]) REFERENCES [dbo].[Inquiry] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[OpenTenderCondition] NOCHECK CONSTRAINT [FK_OpenTenderCondition_Inquiry]
ALTER TABLE [dbo].[OpenTenderCondition] WITH NOCHECK ADD
CONSTRAINT [FK_OpenTenderCondition_OpenTenderCondition] FOREIGN KEY ([unit]) REFERENCES [dbo].[Company] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[OpenTenderCondition] NOCHECK CONSTRAINT [FK_OpenTenderCondition_OpenTenderCondition]
GO
ALTER TABLE [dbo].[OpenTenderCondition] ADD CONSTRAINT [PK__OpenTend__3214EC071652682B] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
