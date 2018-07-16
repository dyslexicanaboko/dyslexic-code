#http://www.americanweddinggroup.com/pluto/571767/Big_571767_0001.jpg
[string]$baseURI = "http://www.americanweddinggroup.com/pluto/571767/Big_571767_"
[string]$fileName = ""
[string]$uri = ""
[string]$target = "C:\Users\eli\Desktop\Rachel's Wedding\"

$clnt = new-object System.Net.WebClient

for ($i=1; $i -le 1000; $i++)
{
	$fileName = [string]::Format("{0:0000}.jpg", $i)
	
	$uri = ($baseURI + $fileName)
		
	$uri

	$clnt.DownloadFile($uri, ($target + $fileName))
}