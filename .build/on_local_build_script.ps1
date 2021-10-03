$networkName = "simple-crud-ntw"
if (!(docker network ls | select-string $networkName -Quiet))
{
	docker network create --driver bridge $networkName
}

$postgresContainerName = "simple-crud-postgres"
if (!(docker ps | select-string $postgresContainerName -Quiet))
{
	docker-compose -f ./docker-compose-infrastructure.yml --env-file ./.env up -d
}