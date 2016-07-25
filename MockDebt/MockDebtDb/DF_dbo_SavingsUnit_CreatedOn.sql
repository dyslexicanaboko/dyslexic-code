ALTER TABLE [dbo].[SavingsUnit]
	ADD	CONSTRAINT DF_dbo_SavingsUnit_CreatedOn 
	DEFAULT CURRENT_TIMESTAMP
	FOR CreatedOn