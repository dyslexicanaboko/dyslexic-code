-- Group perspective
SELECT
	 g.TaskGroupId
    ,g.[Name]
	,g.[Description]
	,g.CreatedOn
	,Tasks = COUNT(l.TaskGroupLinkId)
FROM dbo.TaskGroup g 
	LEFT JOIN dbo.TaskGroupLink l
		ON l.TaskGroupId = g.TaskGroupId
GROUP BY 
	 g.TaskGroupId
    ,g.[Name]
	,g.[Description]
	,g.CreatedOn
ORDER BY 
	g.Name

INSERT INTO dbo.TaskGroup (Name, Description) VALUES ('EmptyGroup', 'This is for testing, no tasks')



-- Task perspective
SELECT
	 t.TaskId
    ,t.Body
	,t.CreatedOn
	,COUNT(l.TaskGroupLinkId) AS Groups
FROM dbo.Task t 
	LEFT JOIN dbo.TaskGroupLink l
		ON t.TaskId = l.TaskId
GROUP BY 
	 t.TaskId
    ,t.Body
	,t.CreatedOn
ORDER BY 
	t.Body