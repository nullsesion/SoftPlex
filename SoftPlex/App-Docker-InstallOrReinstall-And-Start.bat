@echo off

If Exist "docker\.env" (
    Echo .env exist 
) Else (
    copy docker\sample.env docker\.env /a
)

docker-compose -f docker/docker-compose.yml down
TIMEOUT /T 5
docker-compose -f docker/docker-compose.yml build
TIMEOUT /T 5
docker-compose -f docker/docker-compose.yml up -d
TIMEOUT /T 5
start "" http://localhost:5080/swagger/index.html
TIMEOUT /T 5
start "" http://localhost:5081/