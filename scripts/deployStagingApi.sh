docker load -i code-editor-api.tar
docker stop code-editor-api
docker rm code-editor-api
docker run -p 8080:80 -d --name code-editor-api code-editor-api