SELECT * FROM dbo.MasterTaskList;
SELECT * FROM dbo.SubListLineItems;
SELECT * FROM dbo.SubTaskList;

INSERT INTO dbo.MasterTaskList (Description) VALUES ('change oil')

UPDATE dbo.MasterTaskList
SET description = 'feed dogs'
WHERE MasterListID = 2

INSERT INTO dbo.SubTaskList (Description) VALUES ('task list 22')

INSERT INTO dbo.SubListLineItems (SubTaskListID, MasterListID) 
VALUES (2, 2)

SELECT 
	sub.SubTaskListID,
	sub.Description,
	--sli.*,
	mas.MasterListID,
	mas.Description,
	mas.CreationDate,
	mas.ModifiedDate 
FROM dbo.SubTaskList sub
	INNER JOIN dbo.SubListLineItems sli
		ON sub.SubTaskListID = sli.SubTaskListID
	INNER JOIN dbo.MasterTaskList mas
		ON sli.MasterListID = mas.MasterListID
		