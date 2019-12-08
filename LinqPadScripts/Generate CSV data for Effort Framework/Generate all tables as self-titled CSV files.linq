<Query Kind="Program">
  <Connection>
    <ID>eac9ecfc-eff2-4a24-81af-e3ace612cbda</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <IsProduction>true</IsProduction>
    <Database>EmployeeDefense</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

/* Convert table row data into self-titled files as CSV files.
 * This is for the Effort Framework to consume https://entityframework-effort.net/ 
 * The goal is to keep it SIMPLE. */
string BasePath = Path.GetDirectoryName(Util.CurrentQueryPath);
string CsvFiles = @"J:\Dump\CsvFiles\";

void Main()
{
	Directory.CreateDirectory(CsvFiles);
	
	var lstTables = GetTables();
	//var lstTables = new List<string> { "[dbo].[User]" };
	
	GenerateCsvFiles(lstTables);
}

public void GenerateCsvFiles(List<TableInfo> tables)
{
	foreach (var t in tables)
	{
		GenerateCsvFile(t);
		
		Console.WriteLine();
	}
}

public void GenerateCsvFile(TableInfo tableInfo)
{
	var dt = GetData(tableInfo.QualifiedSchemaAndTable);

	Console.WriteLine($"Table: {tableInfo} -> Rows: {dt.Rows.Count:n0}");

	string[] arr = dt.Columns
		.Cast<DataColumn>()
		.Select(x => x.ColumnName)
		.ToArray();

	var columns = string.Join(",", arr);
	
	var sb = new StringBuilder();

	sb.AppendLine(columns);

	foreach (var row in dt.Select())
	{
		for (var c = 0; c < dt.Columns.Count; c++)
		{
			sb.Append(row[c]).Append(",");
		}
		
		//Remove trailing comma
		sb.Remove(sb.Length - 1, 1);
		
		sb.AppendLine();
	}
	
	var text = sb.ToString();

	var saveAs = Path.Combine(CsvFiles, tableInfo.TableName + ".csv");

	Console.WriteLine($"Saved as: {saveAs} -> Bytes: {text.Length:n0}");

	File.WriteAllText(saveAs, text, Encoding.ASCII);
}

public List<TableInfo> GetTables()
{
	using (var r = ExecuteReader("GetTables.sql"))
	{
		var lst = new List<TableInfo>();
		
		while (r.Read())
		{
			var m = new TableInfo();

			m.SchemaName = Convert.ToString(r["SchemaName"]);
			m.TableName = Convert.ToString(r["TableName"]);
			m.QualifiedSchemaAndTable = Convert.ToString(r["QualifiedSchemaAndTable"]);
			
			lst.Add(m);
		}
		
		return lst;
	}
}

public SqlDataReader ExecuteReader(string queryFileName)
{
	var connection = new SqlConnection(this.Connection.ConnectionString);

	connection.Open();

	var query = GetQuery(queryFileName);

	var command = new SqlCommand(query, connection);

	command.CommandType = CommandType.Text;

	var dr = command.ExecuteReader(CommandBehavior.CloseConnection);
	
	return dr;
}

private string GetQuery(string fileName)
{
	var text = File.ReadAllText(Path.Combine(BasePath, fileName));
	
	return text;
}

private DataTable GetData(string schemaAndTableName)
{
	var dt = new DataTable(schemaAndTableName);

	using (var connection = new SqlConnection(this.Connection.ConnectionString))
	{
		connection.Open();

		var query = "SELECT * FROM " + schemaAndTableName;

		try
		{	        
			using (var command = new SqlCommand(query, connection))
			{
				using (var da = new SqlDataAdapter(command))
				{
					da.Fill(dt);
				}
			}
		}
		catch
		{
			Console.WriteLine($"Query -> {query}");
			
			throw;
		}
	}

	return dt;
}

public class TableInfo
{
	public string SchemaName { get; set; }
	public string TableName { get; set; }
	public string QualifiedSchemaAndTable { get; set; }
}