$location = Get-Location
Set-Location $PSScriptRoot

Write-Host "Api.Gateway service integration tests in progress " -ForegroundColor Yellow
docker-compose -f ../.tests/api-gateway-integration-tests.yml --env-file ../.env build #--no-cache
docker-compose -f ../.tests/api-gateway-integration-tests.yml --env-file ../.env up --force-recreate --abort-on-container-exit 
Write-Host "Api.Gateway service integration tests done" -ForegroundColor Green

Write-Host "Gis service integration tests in progress " -ForegroundColor Yellow
docker-compose -f ../.tests/gis-integration-tests.yml --env-file ../.env build #--no-cache
docker-compose -f ../.tests/gis-integration-tests.yml --env-file ../.env up --force-recreate --abort-on-container-exit  
Write-Host "Gis service integration tests done" -ForegroundColor Green

Set-Location $location