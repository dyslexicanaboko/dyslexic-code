USE [PagingSwitch];

SELECT *
FROM dbo.PagingQueue

INSERT INTO PagingQueue 
(
	[SubscriberID]
   ,[MessageText]
   ,[SenderIP]
)
VALUES
(9901, 'Test from SQL Server', 'T.E.S.T');
INSERT INTO PagingQueue 
(
	[SubscriberID]
   ,[MessageText]
   ,[SenderIP]
)
VALUES
(9902, 'Test from SQL Server', 'T.E.S.T');  
INSERT INTO PagingQueue 
(
	[SubscriberID]
   ,[MessageText]
   ,[SenderIP]
)
VALUES
(9902, 'This is from the SQL Server', 'SS');

SELECT *
FROM [PagingSwitch].[dbo].[PagingQueue]
  
UPDATE dbo.PagingQueue SET 
IsSent = 0,
DateTimeSent = NULL
  
SELECT * FROM dbo.Pagers

INSERT INTO dbo.Pagers (SubscriberID, IndividualID, GroupID, MaicdropID, BagID)
VALUES(9903, 0159903, NULL, NULL, 3)  

SELECT * FROM dbo.PagerOwners

INSERT INTO dbo.PagerOwners (FirstName, LastName, EmailAddress, PhoneNumber)
VALUES('Merritt', 'Burrus', NULL, '3054588199')

SELECT * FROM dbo.Groups

INSERT INTO dbo.Groups (GroupDescription)
VALUES('Test Group')

SELECT * FROM dbo.GroupMembers

INSERT INTO dbo.GroupMembers (GroupID, PagerOwnerID)
VALUES
(2, 1),
(2, 2)


/*
Groups {GroupID}
GroupID <-- PagerOwnerID
*/