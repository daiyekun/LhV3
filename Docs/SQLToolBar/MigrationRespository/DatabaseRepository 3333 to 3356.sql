/*
This is the script that will be used to migrate the database from revision 3333 to revision 3356.

You can customize the script, and your edits will be used in deployment.
The following objects will be affected:
  dbo.ContTextHistory, dbo.ContText
*/

SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
PRINT N'Altering [dbo].[ContText]'
GO
ALTER TABLE [dbo].[ContText] ALTER COLUMN [Stage] [int] NULL
GO
PRINT N'Altering [dbo].[ContTextHistory]'
GO
ALTER TABLE [dbo].[ContTextHistory] ALTER COLUMN [Stage] [int] NULL
GO
PRINT N'Adding foreign keys to [dbo].[ContTextHistory]'
GO
ALTER TABLE [dbo].[ContTextHistory] WITH NOCHECK  ADD CONSTRAINT [FK_ContTextHistory_ContTxtTemplateHist] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[ContTxtTemplateHist] ([Id]) NOT FOR REPLICATION
GO
PRINT N'Adding foreign keys to [dbo].[ContText]'
GO
ALTER TABLE [dbo].[ContText] WITH NOCHECK  ADD CONSTRAINT [FK_ContText_ContTxtTemplateHist] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[ContTxtTemplateHist] ([Id]) NOT FOR REPLICATION
GO
PRINT N'Disabling constraints on [dbo].[ContTextHistory]'
GO
ALTER TABLE [dbo].[ContTextHistory] NOCHECK CONSTRAINT [FK_ContTextHistory_ContTxtTemplateHist]
GO
PRINT N'Disabling constraints on [dbo].[ContText]'
GO
ALTER TABLE [dbo].[ContText] NOCHECK CONSTRAINT [FK_ContText_ContTxtTemplateHist]
GO
