# MVVM Architecture with Entity Framework Core Setup

This document describes the MVVM (Model-View-ViewModel) architecture implementation in SajorWPF with Entity Framework Core for database access.

## Architecture Overview

### Folder Structure
```
SajorWPF/
├── Data/
│   └── AppDbContext.cs          # Entity Framework DbContext
├── Models/
│   └── Person.cs                # Entity models
├── Repositories/
│   ├── IPersonRepository.cs     # Repository interface
│   └── PersonRepository.cs      # Repository implementation
├── ViewModels/
│   ├── BaseViewModel.cs         # Base ViewModel with INotifyPropertyChanged
│   └── MainViewModel.cs         # Main window ViewModel
├── Views/
│   ├── MainWindow.xaml          # Main window view
│   └── MainWindow.xaml.cs       # Main window code-behind
├── App.xaml                     # Application definition
├── App.xaml.cs                  # DI container configuration
└── appsettings.json             # Configuration file
```

### Components

#### 1. Models
- **Person.cs**: Entity class representing a person with properties: Id, FirstName, LastName, Age, Position

#### 2. Data Layer
- **AppDbContext.cs**: Entity Framework Core DbContext with DbSet<Person>
- **appsettings.json**: Contains connection string configuration for SQL Server LocalDB

#### 3. Repository Pattern
- **IPersonRepository.cs**: Interface defining CRUD operations
- **PersonRepository.cs**: Implementation using Entity Framework Core

#### 4. ViewModels
- **BaseViewModel.cs**: Base class implementing INotifyPropertyChanged for property change notifications
- **MainViewModel.cs**: ViewModel for MainWindow with commands and business logic

#### 5. Views
- **MainWindow.xaml**: XAML view with data bindings to MainViewModel

#### 6. Dependency Injection
- **App.xaml.cs**: Configures services, DbContext, repositories, and ViewModels using Microsoft.Extensions.DependencyInjection

## Setup Instructions

### Prerequisites
- .NET 9.0 SDK
- SQL Server LocalDB (comes with Visual Studio)
- Visual Studio 2022 or later

### Installation Steps

1. **Restore NuGet Packages**
   ```bash
   dotnet restore
   ```

2. **Update Connection String (if needed)**
   
   Edit `appsettings.json` to match your SQL Server instance:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SajorWPFDb;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

3. **Create Database Migration**
   ```bash
   dotnet ef migrations add InitialCreate
   ```

4. **Update Database**
   ```bash
   dotnet ef database update
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```
   Or press F5 in Visual Studio

## Database Migrations

### Create a New Migration
When you make changes to your models, create a new migration:
```bash
dotnet ef migrations add <MigrationName>
```

Example:
```bash
dotnet ef migrations add AddEmailToEmployee
```

### Apply Migrations
Apply pending migrations to the database:
```bash
dotnet ef database update
```

### Remove Last Migration (if not applied)
```bash
dotnet ef migrations remove
```

### List All Migrations
```bash
dotnet ef migrations list
```

### Rollback to Specific Migration
```bash
dotnet ef database update <MigrationName>
```

## Usage

### Adding a Person
1. Fill in the form fields: First Name, Last Name, Age, Position
2. Click the "Add" button
3. The person will be saved to the database and appear in the list

### Viewing People
All people are automatically loaded from the database when the application starts and displayed in the ListBox.

## Configuration

### Connection String Options

**LocalDB (Default)**
```json
"Server=(localdb)\\mssqllocaldb;Database=SajorWPFDb;Trusted_Connection=true;MultipleActiveResultSets=true"
```

**SQL Server Express**
```json
"Server=.\\SQLEXPRESS;Database=SajorWPFDb;Trusted_Connection=true;MultipleActiveResultSets=true"
```

**SQL Server with Authentication**
```json
"Server=localhost;Database=SajorWPFDb;User Id=your_username;Password=your_password;MultipleActiveResultSets=true"
```

## NuGet Packages Used

- **Microsoft.EntityFrameworkCore** (9.0.0): Core EF functionality
- **Microsoft.EntityFrameworkCore.SqlServer** (9.0.0): SQL Server database provider
- **Microsoft.EntityFrameworkCore.Design** (9.0.0): Design-time components for EF Core
- **Microsoft.EntityFrameworkCore.Tools** (9.0.0): Tools for Package Manager Console
- **Microsoft.Extensions.Configuration** (9.0.0): Configuration framework
- **Microsoft.Extensions.Configuration.Json** (9.0.0): JSON configuration provider
- **Microsoft.Extensions.Configuration.Binder** (9.0.0): Configuration binding
- **Microsoft.Extensions.DependencyInjection** (9.0.0): Dependency injection container
- **Microsoft.Extensions.Hosting** (9.0.0): Hosting infrastructure

## Troubleshooting

### Database Connection Issues
- Ensure SQL Server LocalDB is installed
- Verify the connection string in appsettings.json
- Check if the database exists: `dotnet ef database list`

### Migration Issues
- Delete the Migrations folder and recreate: `dotnet ef migrations add InitialCreate`
- Drop the database and recreate: `dotnet ef database drop` then `dotnet ef database update`

### Build Issues
- Clean and rebuild: `dotnet clean && dotnet build`
- Restore packages: `dotnet restore`

## Further Development

### Adding More Entities
1. Create a new model class in the `Models/` folder
2. Add a DbSet to `AppDbContext.cs`
3. Create repository interface and implementation
4. Register repository in `App.xaml.cs`
5. Create migration: `dotnet ef migrations add Add<EntityName>`
6. Update database: `dotnet ef database update`

### Adding More Views
1. Create XAML and code-behind in `Views/` folder
2. Create corresponding ViewModel in `ViewModels/` folder
3. Register ViewModel and View in `App.xaml.cs` DI container
4. Navigate to the view from your code

## Resources

- [Entity Framework Core Documentation](https://docs.microsoft.com/ef/core/)
- [MVVM Pattern](https://docs.microsoft.com/dotnet/desktop/wpf/data/data-binding-overview)
- [WPF Documentation](https://docs.microsoft.com/dotnet/desktop/wpf/)
- [.NET Dependency Injection](https://docs.microsoft.com/dotnet/core/extensions/dependency-injection)
