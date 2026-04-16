#!/usr/bin/env bash
set -euo pipefail

COMPOSE_FILE=${1:-docker-compose.yml}
DB_NAME=${2:-betstream}
PG_USER=${3:-postgres}

echo "Ensuring Postgres container is running..."
docker-compose -f "$COMPOSE_FILE" up -d postgres >/dev/null

echo "Dropping database '$DB_NAME' if it exists..."
docker-compose -f "$COMPOSE_FILE" exec -T postgres psql -U "$PG_USER" -c "DROP DATABASE IF EXISTS \"$DB_NAME\";"

echo "Creating database '$DB_NAME'..."
docker-compose -f "$COMPOSE_FILE" exec -T postgres psql -U "$PG_USER" -c "CREATE DATABASE \"$DB_NAME\";"

echo "To apply EF migrations run:"
echo "  dotnet tool install --global dotnet-ef --version 8.0.0"
echo "  dotnet ef database update -p BetStream.Application -s BetStream"

echo "Done."
