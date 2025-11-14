# MVVM + Entity Framework Core Setup Guide

This document provides setup and migration instructions for the SajorWPF application using MVVM architecture and Entity Framework Core.

## Architecture Overview

This application implements the Model-View-ViewModel (MVVM) pattern with Entity Framework Core for data persistence.

### Project Structure

```
SajorWPF/
├── Data/
│   └── AppDbContext.cs          # EF Core DbContext
├── Models/
│   └── Person.cs                # Entity model
├── Repositories/
│   ├── IPersonRepository.cs     # Repository interface
│   └── PersonRepository.cs      # Repository implementation
├── ViewModels/
│   ├── BaseViewModel.cs         # Base class for ViewModels
│   └── MainViewModel.cs         # MainWindow ViewModel
├── Views/
│   ├── MainWindow.xaml          # MainWindow view
│   └── MainWindow.xaml.cs       # MainWindow code-behind
├── App.xaml                     # Application definition
├── App.xaml.cs                  # Application startup with DI
├── appsettings.json             # Configuration file
└── README_MVVM_EF.md           # This file
```

## Configuration

### Database Connection String

The database connection string is configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SajorWPFDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

**Note:** This uses SQL Server LocalDB by default. Update the connection string to match your SQL Server instance.

## Setup Instructions

### Prerequisites

1. .NET 9.0 SDK or later
2. SQL Server or SQL Server LocalDB
3. Visual Studio 2022 or later (recommended) or Visual Studio Code

### Installation Steps

1. **Restore NuGet Packages**
   ```bash
   dotnet restore
   ```

2. **Update Connection String** (if needed)
   
   Edit `appsettings.json` and update the `DefaultConnection` to point to your SQL Server instance.

3. **Create Initial Migration**
   ```bash
   dotnet ef migrations add InitialCreate
   ```

4. **Update Database**
   ```bash
   dotnet ef database update
   ```

5. **Build the Application**
   ```bash
   dotnet build
   ```

6. **Run the Application**
   ```bash
   dotnet run
   ```

## Entity Framework Core Migrations

### Common Migration Commands

- **Add a new migration:**
  ```bash
  dotnet ef migrations add <MigrationName>
  ```

- **Update database to latest migration:**
  ```bash
  dotnet ef database update
  ```

- **Rollback to a specific migration:**
  ```bash
  dotnet ef database update <MigrationName>
  ```

- **Remove last migration (if not applied):**
  ```bash
  dotnet ef migrations remove
  ```

- **List all migrations:**
  ```bash
  dotnet ef migrations list
  ```

- **Generate SQL script:**
  ```bash
  dotnet ef migrations script
  ```

## Dependency Injection

The application uses Microsoft.Extensions.DependencyInjection for dependency injection. Services are configured in `App.xaml.cs`:

- **DbContext:** `AppDbContext` registered with SQL Server provider
- **Repositories:** `IPersonRepository` → `PersonRepository`
- **ViewModels:** `MainViewModel`
- **Views:** `MainWindow`

## MVVM Pattern Implementation

### Models
Entity classes representing database tables (e.g., `Person.cs`)

### Views
XAML files with minimal code-behind (e.g., `MainWindow.xaml`)

### ViewModels
- Inherit from `BaseViewModel` for INotifyPropertyChanged implementation
- Contain business logic and data for views
- Use `RelayCommand` for command bindings

### Data Access
- Repository pattern for data access
- Async/await for database operations

## Using the Application

1. **Adding a Person:**
   - Fill in First Name, Last Name, Age, and Position fields
   - Click the "Add" button
   - The person will be saved to the database and displayed in the list

2. **Viewing Persons:**
   - The list automatically loads all persons from the database on startup
   - Each person displays: FirstName LastName - Position (Age: X)

## Troubleshooting

### Connection Issues
- Ensure SQL Server or LocalDB is running
- Verify the connection string in `appsettings.json`
- Check firewall settings if using a remote SQL Server

### Migration Issues
- Ensure EF Core tools are installed: `dotnet tool install --global dotnet-ef`
- Delete the `Migrations` folder and recreate migrations if needed
- Check that the connection string is correct before running migrations

### Build Issues
- Clean and rebuild: `dotnet clean && dotnet build`
- Delete `bin` and `obj` folders and rebuild
- Ensure all NuGet packages are restored

## NuGet Packages Used

- Microsoft.EntityFrameworkCore (9.0.0)
- Microsoft.EntityFrameworkCore.SqlServer (9.0.0)
- Microsoft.EntityFrameworkCore.Design (9.0.0)
- Microsoft.EntityFrameworkCore.Tools (9.0.0)
- Microsoft.Extensions.Configuration (9.0.0)
- Microsoft.Extensions.Configuration.Json (9.0.0)
- Microsoft.Extensions.Configuration.Binder (9.0.0)
- Microsoft.Extensions.DependencyInjection (9.0.0)
- Microsoft.Extensions.Hosting (9.0.0)

## Next Steps

- Add update and delete functionality
- Implement data validation
- Add error handling and logging
- Create additional views and ViewModels
- Implement unit tests for ViewModels and Repositories
