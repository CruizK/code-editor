# Deploy UI
docker load -i code-editor-ui.tar
docker stop code-editor-ui
docker rm code-editor-ui
docker run -p 3000:3000 -d --restart unless-stopped  --name code-editor-ui code-editor-ui

# Deploy db
docker load -i code-editor-db.tar
docker ps -a 
docker run --rm --name code-editor-db --network="host" code-editor-db
docker ps -a

# Deploy api
docker load -i code-editor-api.tar
docker stop code-editor-api
docker rm code-editor-api
docker run -p 8080:80 -d --restart unless-stopped --name code-editor-api --network="db" code-editor-api