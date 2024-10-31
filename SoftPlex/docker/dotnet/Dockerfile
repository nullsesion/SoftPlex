FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Debug
WORKDIR /src

COPY ./src/ .
WORKDIR "/src/."

RUN dotnet restore ./SoftPlex.Api/SoftPlex.Api.csproj

RUN dotnet tool install -g dotnet-ef

RUN dotnet build "./SoftPlex.Api/SoftPlex.Api.csproj" -c $BUILD_CONFIGURATION -o /app/buildapi
RUN find /app/buildapi -type d -print0|xargs -0 chmod ugo+w

#WORKDIR /app/buildapi/.

#RUN apt-get update 
#RUN yes | apt-get install nginx
#RUN apt-get install nginx -y

WORKDIR /app/buildapi/.

RUN sed -i 's/localhost/postgres_container/' /app/buildapi/appsettings.json
RUN sed -i 's/localhost/postgres_container/' /app/buildapi/appsettings.Development.json

RUN sed -i 's/54323/5432/' /app/buildapi/appsettings.json
RUN sed -i 's/54323/5432/' /app/buildapi/appsettings.Development.json



ENTRYPOINT ["dotnet", "/app/buildapi/SoftPlex.Api.dll"]
#CMD ["nginx", "-g", "daemon off;"]
