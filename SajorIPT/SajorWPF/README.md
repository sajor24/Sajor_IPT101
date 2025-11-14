# SajorWPF - MVVM Architecture with Entity Framework Core

This WPF application demonstrates a minimal MVVM architecture with Entity Framework Core for database access.

## Architecture

### MVVM Pattern
- **Models**: Plain C# objects (POCOs) representing data entities
  - `Person.cs` - Example entity with Id, FirstName, LastName, CreatedAt
  
- **Views**: XAML files and code-behind
  - `MainWindow.xaml` - Main UI with data binding to ViewModel
  - `MainWindow.xaml.cs` - Minimal code-behind that sets DataContext via DI

- **ViewModels**: Business logic and state management
  - `BaseViewModel.cs` - Base class implementing INotifyPropertyChanged
  - `MainViewModel.cs` - Main view model with ObservableCollection of People

### Data Access
- **DbContext**: `Data/AppDbContext.cs` - Entity Framework Core context
- **Repository Pattern**: 
  - `IPersonRepository.cs` - Repository interface
  - `PersonRepository.cs` - Repository implementation

### Configuration
- **appsettings.json** - Configuration file with connection strings
- Connection string key: `ConnectionStrings:DefaultConnection`
- Default: SQL Server LocalDB

### Dependency Injection
- Configured in `App.xaml.cs` OnStartup
- Services registered:
  - DbContext (Scoped)
  - Repositories (Scoped)
  - ViewModels (Transient)
  - Views (Transient)

## Setup

1. Update the connection string in `appsettings.json` to point to your SQL Server instance
2. Run migrations to create the database:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
3. Run the application

## NuGet Packages
- Microsoft.EntityFrameworkCore (9.0.0)
- Microsoft.EntityFrameworkCore.SqlServer (9.0.0)
- Microsoft.EntityFrameworkCore.Design (9.0.0)
- Microsoft.EntityFrameworkCore.Tools (9.0.0)
- Microsoft.Extensions.Configuration (9.0.0)
- Microsoft.Extensions.Configuration.Json (9.0.0)
- Microsoft.Extensions.Configuration.Binder (9.0.0)
- Microsoft.Extensions.DependencyInjection (9.0.0)
- Microsoft.Extensions.Hosting (9.0.0)
