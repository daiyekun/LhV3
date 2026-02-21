CREATE TABLE [dbo].[ContConsult]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ContId] [int] NULL,
[UserId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContConsult] ADD CONSTRAINT [PK_ContConsult] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
