CREATE TABLE [dbo].[Company]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Ctype] [tinyint] NULL,
[Name] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Code] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[LevelId] [int] NULL,
[CareditId] [int] NULL,
[CompClassId] [int] NULL,
[CompTypeId] [int] NULL,
[IsDelete] [tinyint] NOT NULL,
[CountryId] [int] NULL,
[ProvinceId] [int] NULL,
[CityId] [int] NULL,
[Trade] [nvarchar] (150) COLLATE Chinese_PRC_CI_AS NULL,
[Address] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[PostCode] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL,
[Tel] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Fax] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[RegisterCapital] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[RegisterAddress] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[FoundDateTime] [datetime] NULL,
[BusinessTerm] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[ExpirationDateTime] [datetime] NULL,
[InvoiceTitle] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[TaxIdentification] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[InvoiceAddress] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[InvoiceTel] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[BankName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[BankAccount] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[PaidUpCapital] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[LegalPerson] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[WebSite] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[FirstContact] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[FirstContactDept] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[FirstContactPosition] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[FirstContactTel] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[FirstContactMobile] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[FirstContactQQ] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[FirstContactEmail] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[Cstate] [tinyint] NOT NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[PrincipalUserId] [int] NULL,
[BusinessScope] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Reserve1] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Reserve2] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[WfState] [tinyint] NULL,
[WfItem] [int] NULL,
[WfCurrNodeName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Company] ADD CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Company] ON [dbo].[Company] ([PrincipalUserId], [CreateUserId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Company] WITH NOCHECK ADD CONSTRAINT [FK_Company_DataDictionary2] FOREIGN KEY ([CareditId]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Company] WITH NOCHECK ADD CONSTRAINT [FK_Company_DataDictionary3] FOREIGN KEY ([CompClassId]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Company] WITH NOCHECK ADD CONSTRAINT [FK_Company_DataDictionary4] FOREIGN KEY ([CompTypeId]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Company] WITH NOCHECK ADD CONSTRAINT [FK_Company_UserInfor1] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Company] WITH NOCHECK ADD CONSTRAINT [FK_Company_DataDictionary1] FOREIGN KEY ([LevelId]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Company] WITH NOCHECK ADD CONSTRAINT [FK_Company_UserInfor2] FOREIGN KEY ([ModifyUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Company] WITH NOCHECK ADD CONSTRAINT [FK_Company_UserInfor3] FOREIGN KEY ([PrincipalUserId]) REFERENCES [dbo].[UserInfor] ([Id])
GO
ALTER TABLE [dbo].[Company] NOCHECK CONSTRAINT [FK_Company_DataDictionary2]
GO
ALTER TABLE [dbo].[Company] NOCHECK CONSTRAINT [FK_Company_DataDictionary3]
GO
ALTER TABLE [dbo].[Company] NOCHECK CONSTRAINT [FK_Company_DataDictionary4]
GO
ALTER TABLE [dbo].[Company] NOCHECK CONSTRAINT [FK_Company_UserInfor1]
GO
ALTER TABLE [dbo].[Company] NOCHECK CONSTRAINT [FK_Company_DataDictionary1]
GO
ALTER TABLE [dbo].[Company] NOCHECK CONSTRAINT [FK_Company_UserInfor2]
GO
ALTER TABLE [dbo].[Company] NOCHECK CONSTRAINT [FK_Company_UserInfor3]
GO
