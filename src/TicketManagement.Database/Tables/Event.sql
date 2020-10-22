CREATE TABLE [dbo].[Event]
(
	[Id] int primary key identity,
	[Name] nvarchar(120) NOT NULL,
	[Description] nvarchar(max) NOT NULL,
	[StartDateTime] datetime NOT NULL,
	[FinishDateTime] datetime NOT NULL,
	[LayoutId] int NOT NULL,
)
