$source=$MyInvocation.MyCommand.Path
$path=Split-Path $source -Parent
$dest=$path+"..\..\ChemSharpUnity\Assets\packages"
$source=$path+"\packages\"
dotnet restore
New-Item $dest -ItemType Directory -Force


$filter = [regex] "lib\\netstandard2.0"
Get-ChildItem -Path $source -Recurse -Include *.dll | Where-Object {$_.FullName -match $filter} | Copy-Item -Destination $dest
