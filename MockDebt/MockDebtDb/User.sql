CREATE TABLE [dbo].[User]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY(1,1)
	,UserName varchar(255) NOT NULL
	,CreatedOn datetime2(0) NOT NULL
)
