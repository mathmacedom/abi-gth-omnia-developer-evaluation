[Back to README](../README.md)

# Setup Guide

Instructions to configure and execute the `Ambev.DeveloperEvaluation` project locally. This sales management API uses .NET 8, PostgreSQL, RabbitMQ, and Docker.

## Requirements

Install these tools on your machine:
- **Docker**: Container management (Docker Desktop recommended)
- **Docker Compose**: Included with Docker Desktop
- **.NET 8 SDK**: For local development (optional with Docker)
- **Git**: Repository cloning
- **IDE**: Visual Studio Code or Visual Studio 2022

Verify installations:
```sh
docker --version
docker-compose --version
dotnet --version
git --version
```

### Step 1: Clone Repository
Download the project:
```bash
git clone https://github.com/mathmacedom/abi-gth-omnia-developer-evaluation.git
cd abi-gth-omnia-developer-evaluation
```

### Step 2: Environment Setup
The project uses Docker Compose to orchestrate services. Ensure these ports are available:

- 8080, 8081 (API)
- 5433 (PostgreSQL) 
- 5672, 15672 (RabbitMQ)

Check port availability:
```
netstat -an | findstr "8080 8081 5433 5672 15672"  # Windows
lsof -i :8080,8081,5432,5672,15672                  # Linux/Mac
```

If ports are occupied, stop conflicting services or modify ports in docker-compose.yml.

### Step 3: Docker Network Creation
Create network for container communication:
```bash
docker network create ambev_developer_evaluation
```

### Step 4: Launch Services
Start the application stack:
```bash
docker-compose up -d
```

This initializes API, PostgreSQL, and RabbitMQ services in background mode.

Confirm containers are running:
```bash
docker ps
```

Expected containers:
* ambev_developer_evaluation_webapi (API)
* ambev_developer_evaluation_database (PostgreSQL)
* ambev_developer_evaluation_rabbitmq (RabbitMQ)

For troubleshooting failed containers:
```bash
docker logs <container_name>
```

#### Network Connection (if required)
Manually connect containers to network if needed:
```bash
docker network connect ambev_developer_evaluation ambev_developer_evaluation_webapi
docker network connect ambev_developer_evaluation ambev_developer_evaluation_database
docker network connect ambev_developer_evaluation ambev_developer_evaluation_rabbitmq
```

Verify network configuration:
```bash
docker network inspect ambev_developer_evaluation
```

### Step 5: Database Initialization
Navigate to `src` directory and execute:
```bash
dotnet ef database update --project Ambev.DeveloperEvaluation.ORM --startup-project Ambev.DeveloperEvaluation.WebApi
```

### Step 6: API Access
Access the Swagger interface at:

URL: http://localhost:8081/swagger

The API supports both HTTPS (8081) and HTTP (8080) endpoints.

## Shutdown
Stop and remove containers:
```bash
docker-compose down
```