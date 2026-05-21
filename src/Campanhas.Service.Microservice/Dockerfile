# =========================
# Base runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# =========================
# Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /appsrc

# Copiar csproj
COPY src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Api/Campanhas.Service.Microservice.Api.csproj src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Api/
COPY src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Application/Campanhas.Service.Microservice.Application.csproj src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Application/
COPY src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Domain/Campanhas.Service.Microservice.Domain.csproj src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Domain/
COPY src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Infra/Campanhas.Service.Microservice.Infra.csproj src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Infra/

# Restore
RUN dotnet restore src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Api/Campanhas.Service.Microservice.Api.csproj

# Copiar todo o código
COPY . .

# Build
WORKDIR /appsrc/src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Api
RUN dotnet build Campanhas.Service.Microservice.Api.csproj -c $BUILD_CONFIGURATION -o /app/build --no-restore

# Publish
RUN dotnet publish Campanhas.Service.Microservice.Api.csproj \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# =========================
# Migrator opcional
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS migrator
WORKDIR /appsrc

COPY . .

WORKDIR /appsrc/src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Api

RUN dotnet tool install --global dotnet-ef --version 9.0.0
ENV PATH="$PATH:/root/.dotnet/tools"

ENTRYPOINT ["dotnet", "ef", "database", "update", "--no-build"]

# =========================
# Runtime final
# =========================
FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Campanhas.Service.Microservice.Api.dll"]