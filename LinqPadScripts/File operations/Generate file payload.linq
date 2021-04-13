<Query Kind="Program" />

void Main()
{
	CreateDummyFiles(5, 5000, @"C:\Dump\Data\", "DummyFile", ".txt");
}

public void CreateDummyFiles(int numberOfFiles, int fileSizesInBytes, string saveTo, string saveAs, string extension)
{
	for (int i = 0; i < numberOfFiles; i++)
	{
		var file = saveAs + i + extension;
		
		var path = Path.Combine(saveTo, file);

		var txt = GetPayLoad(5000);
		
		File.WriteAllText(path, txt);
	}
}

// Define other methods and classes here
public string GetPayLoad(int bytes)
{
	var r = new Random();
	
	var sb = new StringBuilder();
	
	var lineWidth = 0;
	
	for (int i = 0; i < bytes; i++)
	{
		lineWidth++;

		var n = r.Next(255); //ASCII limits

		var b = new byte[]{ Convert.ToByte(n) };

		string s = Encoding.ASCII.GetString(b);

		sb.Append((char)n);

		if (lineWidth == 2000)
		{
			sb.AppendLine();
		}
	}
	
	var txt = sb.ToString();
	
	return txt;
}