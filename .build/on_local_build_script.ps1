$networkName = "simple-crud-ntw"

if (!(docker network ls | select-string $networkName -Quiet))
{
	Write-Host "Trololo"
	docker network create --driver bridge $networkName
}
