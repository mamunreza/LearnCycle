echo Clear all CDSv2 docker items ------
@echo off

docker stop lcrabbitmq

docker rm lcrabbitmq

docker images prune

docker volume prune -f

echo Clear process is finished.
pause