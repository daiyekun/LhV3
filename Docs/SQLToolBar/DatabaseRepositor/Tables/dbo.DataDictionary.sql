CREATE TABLE [dbo].[DataDictionary]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Pid] [int] NULL,
[Name] [nvarchar] (150) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Sort] [int] NULL,
[Dtype] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[FundsNature] [tinyint] NULL,
[CreateUserId] [int] NULL,
[CreateDatetime] [datetime] NULL,
[ModifyUserId] [int] NULL,
[ModifyDatetime] [datetime] NULL,
[DtypeNumber] [int] NULL,
[IsDelete] [tinyint] NULL,
[ShortName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DataDictionary] ADD CONSTRAINT [PK_DataDictionary] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_DataDictionary] ON [dbo].[DataDictionary] ([DtypeNumber]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DataDictionary] WITH NOCHECK ADD CONSTRAINT [FK_Dic_User1] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[DataDictionary] WITH NOCHECK ADD CONSTRAINT [FK_Dic_User2] FOREIGN KEY ([ModifyUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[DataDictionary] NOCHECK CONSTRAINT [FK_Dic_User1]
GO
ALTER TABLE [dbo].[DataDictionary] NOCHECK CONSTRAINT [FK_Dic_User2]
GO
