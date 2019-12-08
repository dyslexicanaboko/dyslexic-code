<Query Kind="Program" />

void Main()
{
	var arr = new[] {
		"First Last",
		"First Middle Last"
	};

	//arr.Dump();

	var lst = new List<Person>(arr.Length);

	foreach (var a in arr)
	{
		var t = a.Split(' ');
		
		var p = new Person();
		
		p.First = t[0];

		if (t.Length == 2)
		{
			p.Last = t[1];
		}
		else
		{
			p.Middle = t[1];
			p.Last = t[2];
		}
		
		lst.Add(p);
	}

	foreach (var p in lst)
	{
		var m = p.Middle == null ? "NULL" : "'" + p.Middle + "'";

		Console.WriteLine($",('{p.First}', {m}, '{p.Last}', NULL, 0)");
	}
}



public class Person
{
	public string First { get; set; }
	public string Middle { get; set; }
	public string Last { get; set; }
}