CREATE PROCEDURE [dbo].[TaskGroup_GetAllWithSummary]
AS
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
RETURN 0
