FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY Campanhas.Service.Microservice.Api/Campanhas.Service.Microservice.Api.csproj Campanhas.Service.Microservice.Api/
COPY Campanhas.Service.Microservice.Application/Campanhas.Service.Microservice.Application.csproj Campanhas.Service.Microservice.Application/
COPY Campanhas.Service.Microservice.Domain/Campanhas.Service.Microservice.Domain.csproj Campanhas.Service.Microservice.Domain/
COPY Campanhas.Service.Microservice.Infra/Campanhas.Service.Microservice.Infra.csproj Campanhas.Service.Microservice.Infra/

RUN dotnet restore Campanhas.Service.Microservice.Api/Campanhas.Service.Microservice.Api.csproj

COPY . .

WORKDIR /src/Campanhas.Service.Microservice.Api

RUN dotnet publish Campanhas.Service.Microservice.Api.csproj \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS migrator
WORKDIR /src

COPY . .

WORKDIR /src/Campanhas.Service.Microservice.Api

RUN dotnet tool install --global dotnet-ef --version 9.0.0
ENV PATH="$PATH:/root/.dotnet/tools"

ENTRYPOINT ["dotnet", "ef", "database", "update", "--no-build"]

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Campanhas.Service.Microservice.Api.dll"]