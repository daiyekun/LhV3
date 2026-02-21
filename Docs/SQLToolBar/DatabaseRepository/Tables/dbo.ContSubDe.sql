CREATE TABLE [dbo].[ContSubDe]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[SubDeliverId] [int] NULL,
[DeliverType] [int] NULL,
[DeliverLocation] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[DeliverUserId] [int] NULL,
[ActualDateTime] [datetime] NULL,
[GuidFileName] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[FolderName] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[Name] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[FileName] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Path] [nvarchar] (300) COLLATE Chinese_PRC_CI_AS NULL,
[Field1] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Field2] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[SubId] [int] NULL,
[CurrDevNumber] [decimal] (28, 6) NULL,
[NotDevNumber] [decimal] (28, 6) NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[DState] [tinyint] NULL,
[IsDelete] [tinyint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContSubDe] ADD CONSTRAINT [PK_ContSubDes] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContSubDe] WITH NOCHECK ADD CONSTRAINT [FK_ContSubDes_UserInfor1] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContSubDe] WITH NOCHECK ADD CONSTRAINT [FK_ContSubDes_DataDictionary] FOREIGN KEY ([DeliverType]) REFERENCES [dbo].[DataDictionary] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContSubDe] WITH NOCHECK ADD CONSTRAINT [FK_ContSubDes_UserInfor] FOREIGN KEY ([DeliverUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContSubDe] WITH NOCHECK ADD CONSTRAINT [FK_ContSubDes_ContSubjectMatter] FOREIGN KEY ([SubId]) REFERENCES [dbo].[ContSubjectMatter] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContSubDe] NOCHECK CONSTRAINT [FK_ContSubDes_UserInfor1]
GO
ALTER TABLE [dbo].[ContSubDe] NOCHECK CONSTRAINT [FK_ContSubDes_DataDictionary]
GO
ALTER TABLE [dbo].[ContSubDe] NOCHECK CONSTRAINT [FK_ContSubDes_UserInfor]
GO
ALTER TABLE [dbo].[ContSubDe] NOCHECK CONSTRAINT [FK_ContSubDes_ContSubjectMatter]
GO
