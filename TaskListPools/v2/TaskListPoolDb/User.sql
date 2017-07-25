CREATE TABLE [dbo].[User]
(
	[UserId] INT NOT NULL IDENTITY(1,1),
	[EmailAddress] varchar(255) NOT NULL,
	[FirstName] varchar(255) NOT NULL,
	[LastName] varchar(255) NOT NULL,
	[CreatedOn] DATETIME2(0) CONSTRAINT DF_dbo_User_CreatedOn DEFAULT (GETDATE()) NOT NULL,
	CONSTRAINT PK_dbo_User_UserId PRIMARY KEY CLUSTERED ( UserId ASC )
)
