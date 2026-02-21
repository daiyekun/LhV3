CREATE TABLE [dbo].[UserInfor]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Pwd] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
[LastName] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[FirstName] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[DisplyName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Sex] [int] NOT NULL,
[Age] [int] NULL,
[Tel] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Mobile] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Email] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[EntryDatetime] [datetime] NULL,
[IdNo] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Address] [nvarchar] (150) COLLATE Chinese_PRC_CI_AS NULL,
[DepartmentId] [int] NULL,
[Sort] [int] NULL,
[State] [int] NULL,
[Remark] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateUserId] [int] NULL,
[CreateDatetime] [datetime] NULL,
[ModifyUserId] [int] NULL,
[ModifyDatetime] [datetime] NULL,
[IsDelete] [int] NULL,
[UStart] [int] NOT NULL,
[Msystem] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[Minfo] [nvarchar] (150) COLLATE Chinese_PRC_CI_AS NULL,
[PhName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[PhPath] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserInfor] ADD CONSTRAINT [PK_UserInfor] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserInfor] WITH NOCHECK ADD CONSTRAINT [FK_UserInfor_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([Id]) NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[UserInfor] NOCHECK CONSTRAINT [FK_UserInfor_Department]
GO
