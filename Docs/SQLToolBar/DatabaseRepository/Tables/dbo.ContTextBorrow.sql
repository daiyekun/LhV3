CREATE TABLE [dbo].[ContTextBorrow]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ContTextId] [int] NULL,
[BorrUser] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[BorrDateTime] [datetime] NULL,
[BorrDeptName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[BorrRemark] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[BorrHandlerUser] [int] NULL,
[BorrNumber] [int] NULL,
[RepayNumber] [int] NULL,
[RepayHandlerUser] [int] NULL,
[RepayDateTime] [datetime] NULL,
[RepayUser] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[ModifyUserId] [int] NOT NULL,
[ModifyDateTime] [datetime] NOT NULL,
[IsDelete] [tinyint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContTextBorrow] ADD CONSTRAINT [PK_ContTextBorrow] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
