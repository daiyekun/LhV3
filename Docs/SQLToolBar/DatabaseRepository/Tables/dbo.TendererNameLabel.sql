CREATE TABLE [dbo].[TendererNameLabel]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TenderId] [int] NULL,
[UserId] [int] NULL,
[TeNameLabel] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Psition] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
[TeDartment] [int] NULL,
[IS_DELETE] [int] NOT NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[TendererNameLabel] WITH NOCHECK ADD
CONSTRAINT [FK_TendererNameLabel_TenderInfor] FOREIGN KEY ([TenderId]) REFERENCES [dbo].[TenderInfor] ([Id]) NOT FOR REPLICATION
ALTER TABLE [dbo].[TendererNameLabel] NOCHECK CONSTRAINT [FK_TendererNameLabel_TenderInfor]
GO
ALTER TABLE [dbo].[TendererNameLabel] ADD CONSTRAINT [PK_TendererNameLabel] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
