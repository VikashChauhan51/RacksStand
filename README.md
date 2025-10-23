# RacksStand
Rack-Based Inventory Management System

## Introduction

This document outlines the requirements and assumptions for a rack-based inventory management system designed to help shopkeepers efficiently manage their stock within racks containing pockets or boxes. The system aims to address the challenges of manually locating products, reducing search time, and preventing lost sales due to misplaced items.

`src>:`

```cmd
dotnet ef migrations add InitialCreate --project  RacksStand.Infrastructure/RacksStand.Infrastructure.csproj --startup-project  RacksStand.Api/RacksStand.Api.csproj

dotnet ef database update --project  RacksStand.Infrastructure/RacksStand.Infrastructure.csproj --startup-project  RacksStand.Api/RacksStand.Api.csproj

```

# Migration creation

```cmd
cd src/RacksStand.Api
dotnet ef migrations add InitialCreate --context ApplicationDbContext --output-dir Domain/Migrations
dotnet ef database update

```

## API -
```
https://localhost:7097/openapi/v1.json
```
