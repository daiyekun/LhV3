CREATE TABLE [dbo].[CompDescription]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CompanyId] [int] NULL,
[Item] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ContentText] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompDescription] ADD CONSTRAINT [PK_CompDescription] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CompDescription] WITH NOCHECK ADD CONSTRAINT [FK_CompDescription_UserInfor1] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[CompDescription] WITH NOCHECK ADD CONSTRAINT [FK_CompDescription_UserInfor2] FOREIGN KEY ([ModifyUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[CompDescription] NOCHECK CONSTRAINT [FK_CompDescription_UserInfor1]
GO
ALTER TABLE [dbo].[CompDescription] NOCHECK CONSTRAINT [FK_CompDescription_UserInfor2]
GO
