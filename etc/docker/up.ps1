docker network create platform --label=platform
docker-compose -f docker-compose.infrastructure.yml up -d
exit $LASTEXITCODE