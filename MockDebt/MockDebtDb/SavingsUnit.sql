CREATE TABLE [dbo].[SavingsUnit]
(
	[SavingsUnitId] INT NOT NULL PRIMARY KEY IDENTITY(1,1)
	,UserId int NOT NULL
	,Name varchar(255) NOT NULL
	,Price decimal(19,4) NOT NULL
	,ItemLink varchar(500) NULL
	,Notes varchar(MAX) NULL
	,CreatedOn datetime2(0) NOT NULL
	-- Not sure yet how to do Pictures - I want to see if I can link to Pintrest
)
