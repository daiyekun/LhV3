CREATE TABLE [dbo].[ContStatistic]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ContId] [int] NULL,
[InvoiceAmount] [decimal] (28, 6) NULL,
[ActualAmount] [decimal] (28, 6) NULL,
[CompInAm] [decimal] (28, 6) NULL,
[CompActAm] [decimal] (28, 6) NULL,
[CompRatio] [decimal] (18, 3) NULL,
[BalaTick] [decimal] (28, 6) NULL,
[ModifyUserId] [int] NULL,
[ModifyDateTime] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContStatistic] ADD CONSTRAINT [PK_ContStatistics] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
