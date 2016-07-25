ALTER TABLE [dbo].[SavingsUnit]
	ADD CONSTRAINT [FK_dbo_User_dbo_SavingsUnit_UserId]
	FOREIGN KEY (UserId)
	REFERENCES [User] (UserId)
