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
WORKDIR /src

# Copiar csproj
COPY Campanhas.Service.Microservice.Api/Campanhas.Service.Microservice.Api.csproj Campanhas.Service.Microservice.Api/
COPY Campanhas.Service.Microservice.Application/Campanhas.Service.Microservice.Application.csproj Campanhas.Service.Microservice.Application/
COPY Campanhas.Service.Microservice.Domain/Campanhas.Service.Microservice.Domain.csproj Campanhas.Service.Microservice.Domain/
COPY Campanhas.Service.Microservice.Infra/Campanhas.Service.Microservice.Infra.csproj Campanhas.Service.Microservice.Infra/

RUN dotnet restore Campanhas.Service.Microservice.Api/Campanhas.Service.Microservice.Api.csproj

# Copiar todo o código
COPY . .

# Build
WORKDIR /src/Campanhas.Service.Microservice.Api
RUN dotnet build Campanhas.Service.Microservice.Api.csproj -c $BUILD_CONFIGURATION -o /app/build --no-restore

# Publish
RUN dotnet publish Campanhas.Service.Microservice.Api.csproj \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# =========================
# Runtime final
# =========================
FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Campanhas.Service.Microservice.Api.dll"]