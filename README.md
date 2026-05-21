# Campanhas Service - Conexao Solidaria

API principal da plataforma Conexao Solidaria.

Este servico e responsavel por:

- Cadastro e autenticacao de usuarios
- Emissao de token JWT
- Autorizacao por roles: `GestorONG` e `Doador`
- Cadastro e edicao de campanhas
- Consulta publica das campanhas ativas
- Registro de intencoes de doacao
- Publicacao do evento de doacao no RabbitMQ

---

## 1. Requisitos para execucao local

Antes de iniciar, instale:

- .NET 9 SDK
- Docker Desktop
- SQL Server via container Docker
- RabbitMQ via container Docker
- Git
- Visual Studio 2022 ou Rider
- `dotnet-ef`

Instalar EF CLI, se necessario:

```bash
dotnet tool install --global dotnet-ef
```

Atualizar EF CLI, se ja existir:

```bash
dotnet tool update --global dotnet-ef
```

---

## 2. Subir infraestrutura local

### 2.1 Criar rede Docker

```bash
docker network create conexao-solidaria-network
```

Caso a rede ja exista, o Docker pode informar erro. Nesse caso, siga para o proximo passo.

---

### 2.2 Subir SQL Server

```bash
docker run -d \
  --name sqlserver-campanhas \
  --network conexao-solidaria-network \
  -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD=YourStrong!Passw0rd" \
  -p 1433:1433 \
  mcr.microsoft.com/mssql/server:2022-latest
```

Validar se o container esta rodando:

```bash
docker ps
```

---

### 2.3 Subir RabbitMQ

```bash
docker run -d \
  --name rabbitmq-conexao-solidaria \
  --network conexao-solidaria-network \
  -p 5672:5672 \
  -p 15672:15672 \
  rabbitmq:3-management
```

Painel RabbitMQ:

```text
http://localhost:15672
```

Credenciais padrao:

```text
guest
guest
```

---

## 3. Configurar connection string

No projeto `Campanhas.Service.Microservice.Api`, configure o arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=CampanhasDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;"
  },
  "RabbitMQ": {
    "Host": "localhost",
    "Username": "guest",
    "Password": "guest",
    "DonationQueue": "donations"
  },
  "Jwt": {
    "Key": "CHAVE_SUPER_SECRETA_CONEXAO_SOLIDARIA_123456789",
    "Issuer": "Campanhas.Service.Microservice",
    "Audience": "Campanhas.Service.Microservice"
  }
}
```

---

## 4. Restaurar pacotes

Na raiz da solucao:

```bash
dotnet restore src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.sln
```

---

## 5. Gerar banco de dados com migrations

Na raiz da solucao, execute:

```bash
dotnet ef database update \
  --project src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Infra \
  --startup-project src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Api \
  --context CampaignDbContext
```

Caso precise criar uma nova migration:

```bash
dotnet ef migrations add AddFirstMigration \
  --project src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Infra \
  --startup-project src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Api \
  --context CampaignDbContext
```

---

## 6. Executar a API localmente

```bash
dotnet run --project src/Campanhas.Service.Microservice/Campanhas.Service.Microservice.Api
```

Swagger:

```text
https://localhost:5001/swagger
```

ou, dependendo da porta configurada:

```text
http://localhost:8080/swagger
```

---

## 7. Endpoints principais

### Auth

```http
POST /api/v1/Auth/register
POST /api/v1/Auth/login
```

### Campaigns

```http
POST /api/v1/Campaigns
PUT /api/v1/Campaigns/{id}
GET /api/v1/Campaigns/{id}
GET /api/v1/Campaigns/public
```

### Donations

```http
POST /api/v1/Donations
```

---

## 8. Fluxo de teste via Swagger

### 8.1 Registrar doador

```json
{
  "fullName": "Doador Teste",
  "email": "doador@teste.com",
  "cpf": "12345678901",
  "password": "Doador@123"
}
```

### 8.2 Login

```json
{
  "email": "doador@teste.com",
  "password": "Doador@123"
}
```

Copie o token JWT retornado.

### 8.3 Autorizar no Swagger

Clique em `Authorize` e informe:

```text
Bearer SEU_TOKEN
```

### 8.4 Criar doacao

```json
{
  "campaignId": "GUID_DA_CAMPANHA",
  "amount": 50
}
```

A API deve:

1. Salvar a doacao
2. Publicar o evento `DonationReceivedEvent` no RabbitMQ
3. Retornar status `202 Accepted`

A API nao atualiza diretamente o valor arrecadado da campanha. Essa tarefa e feita pelo worker.

---

## 9. Executar com Docker Compose

Na raiz do repositorio:

```bash
docker compose up -d --build
```

Validar containers:

```bash
docker ps
```

Swagger:

```text
http://localhost:8080/swagger
```

RabbitMQ:

```text
http://localhost:15672
```

---

## 10. Executar no Kubernetes local

Pre-requisito: Kubernetes habilitado no Docker Desktop.

Validar cluster:

```bash
kubectl get nodes
```

Aplicar arquivos:

```bash
kubectl apply -f k8s/
```

Verificar pods:

```bash
kubectl get pods
```

Verificar servicos:

```bash
kubectl get svc
```

Acessar API, se o service estiver como NodePort na porta 30080:

```text
http://localhost:30080/swagger
```

---

## 11. Observabilidade

A API expoe:

```http
GET /health
GET /metrics
```

O Grafana deve ser acessado conforme o service configurado no Kubernetes ou Docker Compose.

---

## 12. Pipeline CI/CD

O GitHub Actions executa automaticamente em push ou pull request para a branch `main`.

O pipeline realiza:

1. Checkout do codigo
2. Instalacao do .NET
3. Build da solucao
4. Execucao de testes, se existirem
5. Build da imagem Docker
6. Publicacao da imagem no Docker Hub

---

## 13. Imagem Docker

Imagem esperada no Docker Hub:

```text
SEU_USUARIO_DOCKERHUB/campanhas-service-api:latest
```

---

## 14. Observacoes para correcao

Para validar o funcionamento completo, execute tambem o projeto `processador-doacoes-worker-service`, pois ele e responsavel por consumir a fila RabbitMQ e atualizar o valor arrecadado das campanhas.
