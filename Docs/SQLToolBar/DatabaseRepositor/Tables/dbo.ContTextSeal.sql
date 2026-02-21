CREATE TABLE [dbo].[ContTextSeal]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ContTextId] [int] NOT NULL,
[SealId] [int] NULL,
[SealUser] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[SealState] [int] NOT NULL,
[SealNumber] [int] NULL,
[EachNumber] [int] NULL,
[SealTotal] [int] NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContTextSeal] ADD CONSTRAINT [PK_ContTextSeal] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContTextSeal] WITH NOCHECK ADD CONSTRAINT [FK_ContTextSeal_ContText] FOREIGN KEY ([ContTextId]) REFERENCES [dbo].[ContText] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContTextSeal] WITH NOCHECK ADD CONSTRAINT [FK_ContTextSeal_SealManager] FOREIGN KEY ([SealId]) REFERENCES [dbo].[SealManager] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContTextSeal] NOCHECK CONSTRAINT [FK_ContTextSeal_ContText]
GO
ALTER TABLE [dbo].[ContTextSeal] NOCHECK CONSTRAINT [FK_ContTextSeal_SealManager]
GO
