SELECT
	  s.name + '.' + T.name AS FullTableName
FROM sys.tables t
	INNER JOIN sys.schemas s
		ON t.schema_id = s.schema_id
WHERE T.name <> '__MigrationHistory'