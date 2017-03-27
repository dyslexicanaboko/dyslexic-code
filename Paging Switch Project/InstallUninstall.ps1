$serviceEXE = "C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil.exe"
$targetDIR = "C:\Users\Eli\Documents\Projects\BeeperWebApp\PagingSystemServiceTestInstallDir\"
$targetEXE = "PagingSystemWindowsService.exe"
$serviceName = "PagingSystemPoller"
$sourceDIR = "C:\Users\Eli\Documents\Projects\BeeperWebApp\PagingSystemWindowsService\bin\Debug\"

"Stopping Service"
NET STOP $serviceName

"Uninstalling Service"
& $serviceEXE /u ($targetDIR + $targetEXE)

"Copying newer Files Over"
COPY -force ($sourceDIR + "*") $targetDIR

"Installing Service"
& $serviceEXE ($targetDIR + $targetEXE)

"Starting Service"
NET START $serviceName
