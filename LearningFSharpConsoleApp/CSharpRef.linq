<Query Kind="Program" />

void Main()
{
	var s = new TaskSession("9:00", "10:00");
	
	ReportTotalHours(s).Dump();
}

public class TaskSession
{
	public TaskSession(string startTime, string endTime)
	{
		string d = DateTime.Today.ToString("yyyy-MM-dd") + " ";
	
		Start = DateTime.Parse(d + startTime);
		End = DateTime.Parse(d + endTime);
	}
	
	public DateTime Start { get; set; }
	public DateTime End { get; set; }
	
	public double GetElapsedHours()
	{
		return (End - Start).TotalHours;
	}
}

// Define other methods and classes here
public double ReportTotalHours(TaskSession session, DateTime? date = null)
{
	return ReportTotalHours(new List<TaskSession>() { session }, date);
}

public double ReportTotalHours(List<TaskSession> sessions, DateTime? date = null)
{
	if(date.HasValue)
	{
		DateTime d = date.Value;
		
		sessions.ForEach(x => 
		{
			DateTime s = x.Start;
			DateTime e = x.End;
			
			x.Start = new DateTime(d.Year, d.Month, d.Day, s.Hour, s.Minute, s.Second); 
			x.End = new DateTime(d.Year, d.Month, d.Day, e.Hour, e.Minute, e.Second);
		});
	}
	
	return sessions.Sum(x => x.GetElapsedHours());
}