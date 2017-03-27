SELECT
	pgr.SubscriberID
INTO #tempOwners
FROM dbo.GroupMembers mem
	INNER JOIN dbo.PagerOwners own
		ON own.PagerOwnerID = mem.PagerOwnerID
	INNER JOIN dbo.Pagers pgr
		ON pgr.PagerID = own.PagerID
WHERE mem.GroupID = 12;

-- SELECT * FROM #tempOwners

SELECT
	pgr.SubscriberID,
	own.PagerOwnerID,
	own.FirstName, 
	own.LastName,
	own.EmailAddress,
	own.PhoneNumber,
	Added = 1
FROM dbo.GroupMembers mem
	INNER JOIN dbo.PagerOwners own
		ON own.PagerOwnerID = mem.PagerOwnerID
	INNER JOIN dbo.Pagers pgr
		ON pgr.PagerID = own.PagerID
WHERE mem.GroupID = 12
UNION
SELECT
	-1,
	-1,
	pgr.SubscriberID,
	own.PagerOwnerID,
	own.FirstName, 
	own.LastName,
	own.EmailAddress,
	own.PhoneNumber,
	Added = 0
FROM dbo.PagerOwners own
	INNER JOIN dbo.Pagers pgr
		ON pgr.PagerID = own.PagerID
WHERE own.PagerOwnerID not in (SELECT tmp.PagerOwnerID FROM #tempOwners tmp)

SELECT * FROM dbo.Groups WHERE GroupID = 12
SELECT * FROM dbo.GroupMembers WHERE GroupID = 12
EXEC usp_GetExistingGroupAndMembers 12

SELECT * FROM dbo.PagingQueue WHERE PagingQueueID > 6;

DELETE FROM dbo.PagingQueue WHERE PagingQueueID > 6;

SELECT DISTINCT pgr.SubscriberID
    FROM dbo.Groups grp
		INNER JOIN dbo.GroupMembers mem
			ON grp.GroupID = mem.GroupID
		INNER JOIN dbo.PagerOwners own
			ON own.PagerOwnerID = mem.PagerOwnerID
		INNER JOIN dbo.Pagers pgr
			ON pgr.PagerID = own.PagerID
	WHERE grp.GroupID in (1)
	
SELECT * FROM dbo.PagerOwners	
SELECT * FROM dbo.Pagers	