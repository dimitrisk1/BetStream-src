param(
    [string]$ComposeFile = "docker-compose.yml",
    [string]$DbName = "betstream",
    [string]$PgUser = "postgres"
)

Write-Host "Ensuring Postgres container is running..."
docker-compose -f $ComposeFile up -d postgres | Out-Null

Write-Host "Dropping database '$DbName' if it exists..."
docker-compose -f $ComposeFile exec -T postgres psql -U $PgUser -c "DROP DATABASE IF EXISTS \"$DbName\";" 

Write-Host "Creating database '$DbName'..."
docker-compose -f $ComposeFile exec -T postgres psql -U $PgUser -c "CREATE DATABASE \"$DbName\";" 

Write-Host "Optionally run EF migrations to recreate schema:"
Write-Host "  dotnet tool install --global dotnet-ef --version 8.0.0"
Write-Host "  dotnet ef database update -p BetStream.Application -s BetStream"

Write-Host "Done."
