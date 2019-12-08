<Query Kind="Program">
  <Connection>
    <ID>b9a76bc9-f2fa-4fd3-bf8d-9c56d5cc2343</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>EmployeeDefense</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

/* Convert table row data into self-titled files as CSV files.
 * This is for the Effort Framework to consume https://entityframework-effort.net/ 
 * The goal is to keep it SIMPLE. */
string BasePath = Path.GetDirectoryName(Util.CurrentQueryPath);
string CsvFiles = @"D:\Dump\CsvFiles\";

void Main()
{
	var lstTables = GetTables();
	
	GenerateCsvFiles(lstTables);
}

public void GenerateCsvFiles(List<string> tables)
{
	foreach (var t in tables)
	{
		GetSchema(t);
	}
}

public void GenerateCsvFile(string table)
{
	var dt = GetSchema(table);
	
	var columns = string.Join(",", dt.Columns);
	
	var sb = new StringBuilder();

	foreach (var row in dt.Rows)
	{
		foreach (var col in dt.Columns)
		{

		}
	}
}

public List<string> GetTables()
{
	using (var r = ExecuteReader("GetTables.sql"))
	{
		var lst = new List<string>();
		
		while (r.HasRows)
		{
			r.Read();
			
			var s = Convert.ToString(r["FullTableName"]);
			
			lst.Add(s);
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

private DataTable GetSchema(string schemaAndTableName)
{
	var dt = new DataTable(schemaAndTableName);

	using (var connection = new SqlConnection(this.Connection.ConnectionString))
	{
		connection.Open();

		var query = "SELECT * FROM " + schemaAndTableName;

		using (var command = new SqlCommand(query, connection))
		{
			using (var da = new SqlDataAdapter(command))
			{
				da.Fill(dt);
			}
		}
	}

	return dt;
}