# Restaurant Management System

A role-based restaurant management(Chef Module) web application built with ASP.NET Core MVC, ASP.NET Core Web API, Entity Framework Core, and SQL Server.

## Project Overview

This project is designed to manage restaurant operations such as kitchen task tracking, inventory usage, category management, and user authentication. It includes a web application for staff use and an API layer for category operations.

## Author
Pulock Kumar Kundu
pulock7643@gmail.com

## Supervisor
Jahid Hassan
Software Development Department, AIUB


## Features

- Role-based login and authorization
- Admin and chef access control
- Kitchen task management
- Inventory/ingredient tracking
- Category CRUD API
- Entity Framework Core database integration
- Layered architecture with separate projects for Web, API, Data, Entities, Repositories, Models, and Shared utilities

## Tech Stack

- ASP.NET Core MVC
- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- Bootstrap
- HTML, CSS, JavaScript

## Solution Structure

```text
RestaurantManagement.Api      # Web API controllers
RestaurantManagement.Web      # MVC web application
RestaurantManagement.Data     # DbContext and database configuration
RestaurantManagement.Entities # Entity/model classes
RestaurantManagement.Repos    # Repository layer
RestaurantManagement.Models   # View/input models
RestaurantManagement.Shared   # Shared helper/result classes
```

## How to Run Locally

1. Install .NET 8 SDK and SQL Server.
2. Clone this repository.
3. Copy the example development settings:

```bash
cp RestaurantManagement.Web/appsettings.Development.example.json RestaurantManagement.Web/appsettings.Development.json
cp RestaurantManagement.Api/appsettings.Development.example.json RestaurantManagement.Api/appsettings.Development.json
```

4. Update the connection string in both `appsettings.Development.json` files according to your SQL Server setup.
5. Open the solution in Visual Studio.
6. Restore NuGet packages.
7. Run the web project or API project.

## Database

The project uses Entity Framework Core with SQL Server. Update the connection string before running the application.

## Database Setup


1. Create a SQL Server database named RestaurantManagementDB.
2. Open the file database/RestaurantManagementDB.sql.
3. Run the SQL script in SQL Server Management Studio.
4. Update the connection string in appsettings.json.
5. Run the project.

Example:

```json
"ConnectionStrings": {
  "RmDbContext": "Server=YOUR_SERVER;Database=RestaurantManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```


