/*
This is the script that will be used to migrate the database from revision 3061 to revision 3149.

You can customize the script, and your edits will be used in deployment.
The following objects will be affected:
  dbo.ContSubjectMatterHistory, dbo.ContSubjectMatter
*/

SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
PRINT N'Altering [dbo].[ContSubjectMatter]'
GO
ALTER TABLE [dbo].[ContSubjectMatter] ADD
[DelNum] [decimal] (28, 6) NULL,
[SjJfRq] [datetime] NULL
GO
PRINT N'Altering [dbo].[ContSubjectMatterHistory]'
GO
ALTER TABLE [dbo].[ContSubjectMatterHistory] ADD
[DelNum] [decimal] (28, 6) NULL,
[SjJfRq] [datetime] NULL
GO
PRINT N'Adding foreign keys to [dbo].[ContSubjectMatter]'
GO
ALTER TABLE [dbo].[ContSubjectMatter] WITH NOCHECK  ADD CONSTRAINT [FK_ContSubjectMatter_BcInstance] FOREIGN KEY ([BcInstanceId]) REFERENCES [dbo].[BcInstance] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[ContSubjectMatter] WITH NOCHECK  ADD CONSTRAINT [FK_ContSubjectMatter_ContractInfo] FOREIGN KEY ([ContId]) REFERENCES [dbo].[ContractInfo] ([Id]) NOT FOR REPLICATION
GO
PRINT N'Disabling constraints on [dbo].[ContSubjectMatter]'
GO
ALTER TABLE [dbo].[ContSubjectMatter] NOCHECK CONSTRAINT [FK_ContSubjectMatter_BcInstance]
ALTER TABLE [dbo].[ContSubjectMatter] NOCHECK CONSTRAINT [FK_ContSubjectMatter_ContractInfo]
GO
