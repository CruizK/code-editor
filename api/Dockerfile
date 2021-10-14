FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

# We only care about the actual web project
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./

RUN dotnet publish CodeEditorApi -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "CodeEditorApi.dll"]
