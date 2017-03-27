SELECT * FROM dbo.PagerOwners

SELECT
	own.PagerOwnerID,
	own.FirstName,
	own.LastName,
	own.EmailAddress,
	own.PhoneNumber,
	pgr.SubscriberID
FROM dbo.PagerOwners own
	INNER JOIN dbo.Pagers pgr
		ON pgr.PagerID = own.PagerID


SELECT * FROM dbo.Groups

INSERT INTO dbo.Groups (GroupDescription)
VALUES('Test Group')

SELECT * FROM dbo.GroupMembers

INSERT INTO dbo.GroupMembers (GroupID, PagerOwnerID)
VALUES
(2, 1),
(2, 2)

SELECT
	grp.GroupID,
	grp.GroupDescription,
	grp.CreatedDTM,
	mem.GroupMemberID,
	pgr.SubscriberID,
	own.PagerOwnerID,
	own.FirstName, 
	own.LastName,
	own.EmailAddress
FROM dbo.Groups grp
	INNER JOIN dbo.GroupMembers mem
		ON grp.GroupID = mem.GroupID
	INNER JOIN dbo.PagerOwners own
		ON own.PagerOwnerID = mem.PagerOwnerID
	INNER JOIN dbo.Pagers pgr
		ON pgr.PagerID = own.PagerID
		
SELECT * FROM dbo.GroupMembers WHERE GroupID = 11;