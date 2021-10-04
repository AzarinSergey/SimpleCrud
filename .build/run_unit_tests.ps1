$location = Get-Location
Set-Location $PSScriptRoot

dotnet test ..\Services\Gis\Gis.Test.Unit\Gis.Test.Unit.csproj

Set-Location $location