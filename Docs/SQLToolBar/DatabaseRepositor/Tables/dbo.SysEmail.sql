CREATE TABLE [dbo].[SysEmail]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ServiceName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[ServicePort] [int] NULL,
[SenderMail] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[SendNickname] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[MailPwd] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysEmail] ADD CONSTRAINT [PK_SysEmail] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
