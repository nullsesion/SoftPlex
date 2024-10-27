FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Debug
WORKDIR /src

COPY ./src/ .
WORKDIR "/src/."

#RUN sed -i 's/localhost/postgres_container/' /app/buildapi/appsettings.json
#RUN sed -i 's/localhost/postgres_container/' /app/buildapi/appsettings.Development.json

RUN sed -i 's/localhost:7044/webapi:8080/' /src/SoftPlex.WebApp/Services/ClientService.cs

RUN dotnet build "./SoftPlex.WebApp/SoftPlex.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/buildapi
RUN find /app/buildapi -type d -print0|xargs -0 chmod ugo+w

#WORKDIR /app/buildapi/.

#RUN apt-get update 
#RUN yes | apt-get install nginx
#RUN apt-get install nginx -y

WORKDIR /app/buildapi/.

ENTRYPOINT ["dotnet", "/app/SoftPlex.WebApp/SoftPlex.Api.dll"]
#CMD ["nginx", "-g", "daemon off;"]
