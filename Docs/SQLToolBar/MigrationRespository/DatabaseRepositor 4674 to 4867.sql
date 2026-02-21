/*
This is the script that will be used to migrate the database from revision 4674 to revision 4867.

You can customize the script, and your edits will be used in deployment.
The following objects will be affected:
  dbo.Inquiry, dbo.OpeningSituationInfor, dbo.ContractInfoHistory,
  dbo.ContTxtTemplateHist, dbo.TenderInfor, dbo.Bidlabel,
  dbo.OpenTenderCondition, dbo.Questioning, dbo.ContTxtTemplate, dbo.OpenBid,
  dbo.TheWinningUnit, dbo.SuccessfulBidderLable, dbo.ContractInfo
*/

SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
PRINT N'Altering [dbo].[Inquiry]'
GO
ALTER TABLE [dbo].[Inquiry] ADD
[Zbdw] [int] NULL,
[Zje] [decimal] (28, 6) NULL
GO
PRINT N'Altering [dbo].[ContractInfoHistory]'
GO
ALTER TABLE [dbo].[ContractInfoHistory] ADD
[ContSingNo] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL
GO
PRINT N'Altering [dbo].[TenderInfor]'
GO
ALTER TABLE [dbo].[TenderInfor] ADD
[Zbdw] [int] NULL,
[Zje] [decimal] (28, 6) NULL
GO
PRINT N'Altering [dbo].[OpeningSituationInfor]'
GO
ALTER TABLE [dbo].[OpeningSituationInfor] ADD
[OpenId] [int] NULL
GO
PRINT N'Altering [dbo].[ContTxtTemplateHist]'
GO
ALTER TABLE [dbo].[ContTxtTemplateHist] ADD
[TepTypes] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL
GO
PRINT N'Altering [dbo].[ContractInfo]'
GO
ALTER TABLE [dbo].[ContractInfo] ADD
[Zbid] [int] NULL,
[Xjid] [int] NULL,
[Ytid] [int] NULL,
[ContSingNo] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL
GO
PRINT N'Altering [dbo].[Questioning]'
GO
ALTER TABLE [dbo].[Questioning] ADD
[Zbdw] [int] NULL,
[Zje] [decimal] (28, 6) NULL
GO
PRINT N'Altering [dbo].[Bidlabel]'
GO
ALTER TABLE [dbo].[Bidlabel] ADD
[Zbdwid] [int] NULL
GO
PRINT N'Altering [dbo].[OpenBid]'
GO
ALTER TABLE [dbo].[OpenBid] ADD
[OpenId] [int] NULL
GO
PRINT N'Altering [dbo].[OpenTenderCondition]'
GO
ALTER TABLE [dbo].[OpenTenderCondition] ADD
[OpenId] [int] NULL,
[Lxr] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Lxfs] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL
GO
PRINT N'Altering [dbo].[ContTxtTemplate]'
GO
ALTER TABLE [dbo].[ContTxtTemplate] ADD
[TepTypes] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL
GO
PRINT N'Altering [dbo].[TheWinningUnit]'
GO
ALTER TABLE [dbo].[TheWinningUnit] ADD
[Zbdwid] [int] NULL,
[Lxr] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Lxfs] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
GO
PRINT N'Altering [dbo].[SuccessfulBidderLable]'
GO
ALTER TABLE [dbo].[SuccessfulBidderLable] ADD
[Zbdwid] [int] NULL
GO
PRINT N'Adding foreign keys to [dbo].[TenderInfor]'
GO
ALTER TABLE [dbo].[TenderInfor] WITH NOCHECK  ADD CONSTRAINT [FK_TenderInfor_Company] FOREIGN KEY ([Zbdw]) REFERENCES [dbo].[Company] ([Id]) NOT FOR REPLICATION
GO
PRINT N'Adding foreign keys to [dbo].[Inquiry]'
GO
ALTER TABLE [dbo].[Inquiry] ADD CONSTRAINT [FK_Inquiry_Company] FOREIGN KEY ([Zbdw]) REFERENCES [dbo].[Company] ([Id])
GO
PRINT N'Adding foreign keys to [dbo].[Questioning]'
GO
ALTER TABLE [dbo].[Questioning] ADD CONSTRAINT [FK_Questioning_Company] FOREIGN KEY ([Zbdw]) REFERENCES [dbo].[Company] ([Id])
GO
PRINT N'Adding foreign keys to [dbo].[ContractInfo]'
GO
ALTER TABLE [dbo].[ContractInfo] ADD CONSTRAINT [FK_ContractInfo_TenderInfor] FOREIGN KEY ([Zbid]) REFERENCES [dbo].[TenderInfor] ([Id])
ALTER TABLE [dbo].[ContractInfo] ADD CONSTRAINT [FK_ContractInfo_Inquiry] FOREIGN KEY ([Xjid]) REFERENCES [dbo].[Inquiry] ([Id])
ALTER TABLE [dbo].[ContractInfo] ADD CONSTRAINT [FK_ContractInfo_Questioning] FOREIGN KEY ([Ytid]) REFERENCES [dbo].[Questioning] ([Id])
GO
PRINT N'Disabling constraints on [dbo].[TenderInfor]'
GO
ALTER TABLE [dbo].[TenderInfor] NOCHECK CONSTRAINT [FK_TenderInfor_Company]
GO
