FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Debug
WORKDIR /src

COPY ./src/ .
WORKDIR "/src/."

RUN sed -i 's/localhost:7044/webapi:8080/' /src/SoftPlex.WebApp/Services/ClientService.cs
RUN sed -i 's/https:/http:/' /src/SoftPlex.WebApp/Services/ClientService.cs


RUN dotnet build "./SoftPlex.WebApp/SoftPlex.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/buildapp

RUN find /app/buildapp -type d -print0|xargs -0 chmod ugo+w

RUN sed -i 's/localhost:7044/webapi:8080/' /app/buildapp/appsettings.json
RUN sed -i 's/https:/http:/' /app/buildapp/appsettings.json
RUN sed -i 's/localhost:7044/webapi:8080/' /app/buildapp/appsettings.Development.json
RUN sed -i 's/https:/http:/' /app/buildapp/appsettings.Development.json

WORKDIR /app/buildapp/
ENTRYPOINT ["dotnet", "/app/buildapp/SoftPlex.WebApp.dll"]

