CREATE TABLE [dbo].[TaskGroupLink]
(
	[TaskGroupLinkId] UNIQUEIDENTIFIER CONSTRAINT DF_dbo_TaskGroupLink_TaskGroupLinkId DEFAULT NEWID() NOT NULL,
	[TaskGroupId] INT NOT NULL,
	[TaskId] INT NOT NULL,
	CONSTRAINT PK_dbo_TaskGroupLink_TaskGroupLinkId PRIMARY KEY CLUSTERED ( TaskGroupLinkId ASC ),
)

GO

ALTER TABLE [dbo].[TaskGroupLink] ADD CONSTRAINT [FK_dbo_TaskGroup_TaskGroupId] FOREIGN KEY([TaskGroupId])
REFERENCES [dbo].[TaskGroup] ([TaskGroupId])

GO

ALTER TABLE [dbo].[TaskGroupLink] ADD CONSTRAINT [FK_dbo_TaskGroupLink_TaskId] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Task] ([TaskId])

GO