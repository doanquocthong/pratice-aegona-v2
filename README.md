# Practice Aegona V2

A simple ASP.NET Core MVC application built with .NET 8.0, implementing authentication, user management, and product management features.

## Technologies

- .NET 8.0
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- JWT Authentication

## Packages

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Swashbuckle.AspNetCore
- Microsoft.AspNetCore.Authentication.JwtBearer

## Project Structure

```text
Practice-Aegona-V2
│
├── Controllers
│   ├── AuthController.cs
│   ├── HomeController.cs
│   ├── ProductController.cs
│   └── UserController.cs
│
├── Data
├── Migrations
├── Models
├── Services
├── Views
├── wwwroot
│
├── appsettings.json
├── appsettings.Development.json
├── Program.cs
└── README.md
```

## Controllers

### AuthController
Handles authentication and authorization operations.

### UserController
Manages user-related features and APIs.

### ProductController
Manages product-related features and APIs.

### HomeController
Provides default MVC pages and application entry points.

## Features

- User Authentication with JWT
- User Management
- Product Management
- Swagger API Documentation
- Entity Framework Core Integration
- SQL Server Database Support

## Run the Project

```bash
dotnet restore
dotnet ef database update
dotnet run
```

Swagger UI:

```text
https://localhost:7278/swagger
```
