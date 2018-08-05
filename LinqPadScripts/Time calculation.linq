<Query Kind="Program" />

void Main()
{
	CalculateSpan("2017-04-01");
	//GetMonthCount("2018-03-01", "2018-02-01").Dump(); //Exception
	//GetMonthCount("2018-03-01", "2018-03-01").Dump();
	//GetMonthCount("2018-02-01", "2018-03-01").Dump();
}

// Define other methods and classes here
public void CalculateSpan(string dateTimeStart)
{
	var dtmEnd = DateTime.Today;
	var dtmStart = DateTime.Parse(dateTimeStart);
	var ts = (dtmEnd - dtmStart);
	var months = GetMonthCount(dtmStart, dtmEnd);

	//ts.Dump();

	Console.WriteLine($"Start: {dtmStart}");
	Console.WriteLine($"End: {dtmEnd}");
	Console.WriteLine($"Days: {ts.Days}");
	Console.WriteLine("Percent of year: {0:p2}", ((decimal)ts.Days / 365.0M));
	Console.WriteLine($"Weeks: {(ts.Days / 7)}");
	Console.WriteLine($"Months: {months}");
}

public int GetMonthCount(string start, string end)
{
	var s = DateTime.Parse(start);
	var e = DateTime.Parse(end);

	return GetMonthCount(s, e);
}

public int GetMonthCount(DateTime start, DateTime end)
{
	var dStart = FirstOfMonth(start);
	var dEnd = FirstOfMonth(end.Date);

	if(start == end) return 0;

	if (start > end)
		throw new Exception($"Start date \"{start.ToShortDateString()}\" cannot be greater than end date \"{end.ToShortDateString()}\"");
	
	var m = 0;
		
	for (var d = dStart; d < dEnd; d = d.AddMonths(1))
	{
		m++;
	}
	
	return m;
}

public DateTime FirstOfMonth(DateTime date)
{
	var d = date;
	
	var dtm = new DateTime(d.Year, d.Month, 1);
	
	return dtm;
}