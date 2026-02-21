CREATE TABLE [dbo].[ProjDescription]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ProjectId] [int] NULL,
[Pitem] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[ProjContent] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProjDescription] ADD CONSTRAINT [PK_ProjDescription] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProjDescription] WITH NOCHECK ADD CONSTRAINT [FK_ProjDescription_UserInfor1] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[UserInfor] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ProjDescription] NOCHECK CONSTRAINT [FK_ProjDescription_UserInfor1]
GO
