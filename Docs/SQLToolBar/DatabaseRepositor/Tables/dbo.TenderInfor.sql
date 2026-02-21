CREATE TABLE [dbo].[TenderInfor]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TenderUserId] [int] NOT NULL,
[ProjectId] [int] NOT NULL,
[ProjectNO] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Iocation] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[TenderDate] [datetime] NOT NULL,
[ContractEnforcementDepId] [int] NOT NULL,
[WinningConditions] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[RecorderId] [int] NOT NULL,
[TenderExpirationDate] [datetime] NOT NULL,
[TenderStatus] [int] NOT NULL,
[IS_DELETE] [int] NOT NULL,
[CreateUserId] [int] NOT NULL,
[TenderType] [int] NULL,
[WfItem] [int] NULL,
[WfCurrNodeName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Zbdw] [int] NULL,
[Zje] [decimal] (28, 6) NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[TenderInfor] WITH NOCHECK ADD
CONSTRAINT [FK_TenderInfor_Company] FOREIGN KEY ([Zbdw]) REFERENCES [dbo].[Company] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[TenderInfor] NOCHECK CONSTRAINT [FK_TenderInfor_Company]
GO
ALTER TABLE [dbo].[TenderInfor] ADD CONSTRAINT [PK__TenderIn__3214EC07B2232990] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TenderInfor] WITH NOCHECK ADD CONSTRAINT [FK_TenderInfor_UserInfor] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[TenderInfor] WITH NOCHECK ADD CONSTRAINT [FK_TenderInfor_ProjectManager] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[ProjectManager] ([Id])
GO
ALTER TABLE [dbo].[TenderInfor] NOCHECK CONSTRAINT [FK_TenderInfor_UserInfor]
GO
