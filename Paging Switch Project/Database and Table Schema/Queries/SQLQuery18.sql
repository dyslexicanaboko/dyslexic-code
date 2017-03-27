INSERT INTO dbo.PagerOwners (PagerID, FirstName, LastName, EmailAddress, PhoneNumber, AdditionalInfo) 
VALUES (-1, 'b', 'b', '', '', '');

EXEC usp_GetPagerOwners

DELETE FROM dbo.PagerOwners WHERE PagerOwnerID > 6;

SELECT CONVERT(nvarchar(10), -1)

EXEC usp_GetMessages @messageType = 1

EXEC usp_GetMessages 1 @messageType = 1