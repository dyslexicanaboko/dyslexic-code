<Query Kind="Program" />

private const int HeaderRows = 2;

void Main()
{
	var strPath = @"J:\Dump\LogEntryTest.txt";
	
	var txt = File.ReadAllText(strPath);
	
	ParseLogForDay(txt);
}

private void ParseLogForDay(string logText)
{
	//Break it down by line
	var lines = logText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
	
	//First row is the log date
	var date = Convert.ToDateTime(lines[0]);
	
	//date.Dump();
	
	//Skip the first two rows because they aren't needed past this point
	lines = lines
		.Skip(HeaderRows)
		.Take(lines.Length - HeaderRows)
		.ToArray();

	//lines.Dump();

	var dayLog = GetIdentifyingData(date, lines);
	
	FillInSessionNotes(dayLog, lines);
	
	dayLog.Dump();
}

public void FillInSessionNotes(DayLog dayLog, string[] lines)
{
	var l = dayLog.Session;
	
	for (int i = 0; i < l.Count; i++)
	{
		var s = l[i];
		var start = s.IdentifyingRow;
		var end = ((i + 1) == l.Count) ? lines.Length : l[i + 1].IdentifyingRow;
		
		var sb = new StringBuilder();
		
		//Grab all of the rows in between the identifying rows because these are the notes
		for (var j = start + 1; j < end; j++)
		{
			sb.AppendLine(lines[j]);
		}
		
		s.Notes = sb.ToString();
	}
}

public DayLog GetIdentifyingData(DateTime logDate, string[] lines)
{
	var dl = new DayLog 
	{
		LogDay = logDate
	};

	//Regex for identifying row
	var rWorkItem1 = new Regex(@"T[0-9]{2}-([0-9]+) \[([A-Za-z0-9 -]+)\] ([0-9]{2}:[0-9]{2}) - ([0-9]{2}:[0-9]{2})");
	var rWorkItem2 = new Regex(@"T[0-9]{2}-([0-9]+) \[([A-Za-z0-9 -]+)\] ([0-9]{2}/[0-9]{2}) ([0-9]{2}:[0-9]{2}) - ([0-9]{2}/[0-9]{2}) ([0-9]{2}:[0-9]{2})");

	var lst = new List<int>();

	for (var i = 0; i < lines.Length; i++)
	{
		var betweenDays = false;
		
		var l = lines[i];
		
		var m = rWorkItem1.Match(l);

		//If the first regex failed
		if (!m.Success)
		{
			//Then try the second regex
			m = rWorkItem2.Match(l);
			
			//If the second one fails then continue
			if(!m.Success) continue;
			
			//Flag the second one as the right one
			betweenDays = true;
		}

		var s = new DayLogSession 
		{
			IdentifyingRow = i,
			WorkItemId = Convert.ToInt32(m.Groups[1].Value),
			Category = m.Groups[2].Value
		};

		if(betweenDays)
		{
			var y = logDate.Year.ToString();

			var d1 = DateTime.Parse(m.Groups[3].Value + $"/{y}"); //05/31/2018
			var d2 = DateTime.Parse(m.Groups[5].Value + $"/{y}"); //06/01/2018

			s.Start = ConstructDate(d1, m.Groups[4].Value);
			s.End = ConstructDate(d2, m.Groups[6].Value);
		}
		else
		{
			s.Start = ConstructDate(logDate, m.Groups[3].Value);
			s.End = ConstructDate(logDate, m.Groups[4].Value);
		}
		
		dl.Session.Add(s);
	}
	
	return dl;
}

private DateTime ConstructDate(DateTime date, string time)
{
	var s = date.ToString("yyyy-MM-dd") + " " + time;
	
	var d = Convert.ToDateTime(s);
	
	return d;
}

public class DayLog
{ 
	public DateTime LogDay { get; set; }
	
	public List<DayLogSession> Session { get; set; } = new List<DayLogSession>();
}

public class DayLogSession
{
	public int IdentifyingRow { get; set; }
	
	public int WorkItemId { get; set; }

	public string Category { get; set; }

	public DateTime Start { get; set; }
	
	public DateTime End { get; set; }
	
	public string Notes { get; set; }
}

// Define other methods and classes here
/*
First line is alway the date
Second line is always the equal signs row
The remaining lines are broken down into session entries and delimeted by a blank line

Session entry model 1:
	[TFS ID] [HH:mm] - [HH:mm]
	Session notes

Session entry model 2:
	[TFS ID] [MM/dd] [HH:mm] - [MM/dd] [HH:mm]
	Session notes
*/