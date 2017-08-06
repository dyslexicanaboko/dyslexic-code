CREATE TABLE [dbo].[TaskGroup]
(
	[TaskGroupId] INT NOT NULL IDENTITY(1,1),
	[Name] varchar(255) NOT NULL,
	[Description] varchar(500) NOT NULL,
	[CreatedOn] DATETIME2(0) CONSTRAINT DF_dbo_TaskGroup_CreatedOn DEFAULT (GETDATE()) NOT NULL,
	CONSTRAINT PK_dbo_TaskGroup_TaskGroupId PRIMARY KEY CLUSTERED ( TaskGroupId ASC )
)
