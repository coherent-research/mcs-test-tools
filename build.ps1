# This script will build the 2 tools and ZIP them up into $zipPath

$msbuild = 'C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe'
$mcsRequestClientPublishPath = 'C:\Development\McsTestTools\McsRequestClient\bin\Release\netcoreapp2.2\publish\'
$mcsResultHostPublishPath = 'C:\Development\McsTestTools\McsResultHost\bin\Release\netcoreapp2.2\publish\'
$zipPath = 'C:\Temp'

Write-Host "Cleaning"
& $msbuild '/verbosity:m', '/target:Clean', '/property:Configuration=Release'

Write-Host "Building"
& $msbuild '/verbosity:m', '/target:Publish', '/property:Configuration=Release'

Write-Host 

$dest = "$zipPath\McsRequestClient.zip"
Write-Host "Zipping McsRequestClient to $dest"
Compress-Archive -Path $mcsRequestClientPublishPath\* -DestinationPath $dest -Force

Write-Host 

$dest = "$zipPath\McsResultHost.zip"
Write-Host "Zipping McsResultHost to $dest"
Compress-Archive -Path $mcsResultHostPublishPath\* -DestinationPath $dest -Force




