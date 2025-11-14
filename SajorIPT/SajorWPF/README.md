# SajorWPF - MVVM Architecture with Entity Framework Core

This project demonstrates a minimal MVVM (Model-View-ViewModel) architecture in a WPF application using Entity Framework Core for database access and Microsoft.Extensions.DependencyInjection for dependency injection.

## Architecture Overview

The application follows the MVVM pattern with a clean separation of concerns:

- **Models**: Data entities (e.g., `Person`)
- **Views**: XAML UI definitions and minimal code-behind
- **ViewModels**: Business logic and data binding support
- **Data**: Entity Framework DbContext
- **Repositories**: Data access abstraction layer

## Project Structure

```
SajorWPF/
├── Data/
│   └── AppDbContext.cs          # Entity Framework DbContext
├── Models/
│   └── Person.cs                # Data model
├── Repositories/
│   ├── IPersonRepository.cs     # Repository interface
│   └── PersonRepository.cs      # Repository implementation
├── ViewModels/
│   ├── BaseViewModel.cs         # Base class with INotifyPropertyChanged
│   └── MainViewModel.cs         # Main window ViewModel
├── Views/
│   ├── MainWindow.xaml          # Main window UI
│   └── MainWindow.xaml.cs       # Main window code-behind
├── App.xaml                     # Application definition
├── App.xaml.cs                  # Application startup with DI configuration
└── appsettings.json             # Configuration file
```

## Features

### 1. Configuration Management
- Uses `appsettings.json` for configuration
- Connection strings loaded via `Microsoft.Extensions.Configuration`
- Supports configuration reload on file changes

### 2. Entity Framework Core
- DbContext configured for SQL Server
- Connection string loaded from configuration
- Repository pattern for data access

### 3. Dependency Injection
- Services registered in `App.xaml.cs`
- DbContext, repositories, and ViewModels resolved via DI
- MainWindow instantiated with dependencies from service provider

### 4. MVVM Pattern
- `BaseViewModel` provides `INotifyPropertyChanged` implementation
- ViewModels expose `ObservableCollection<T>` for data binding
- Views bind to ViewModel properties

## Configuration

### Connection String

Edit `appsettings.json` to configure your database connection:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SajorWPFDb;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

For SQL Server LocalDB (default):
- Ensure SQL Server LocalDB is installed
- The database will be created automatically on first run

For a remote SQL Server:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your-server;Database=SajorWPFDb;User Id=your-user;Password=your-password;TrustServerCertificate=true;"
  }
}
```

## Database Setup

### Creating the Database

1. **Install Entity Framework Core Tools**:
   ```bash
   dotnet tool install --global dotnet-ef
   ```

2. **Create Initial Migration**:
   ```bash
   cd SajorIPT/SajorWPF
   dotnet ef migrations add InitialCreate
   ```

3. **Update Database**:
   ```bash
   dotnet ef database update
   ```

### Adding Sample Data

You can add sample data directly to the database or extend the repository with seed data.

## Building and Running

### Prerequisites
- .NET 9.0 SDK
- SQL Server or SQL Server LocalDB

### Build
```bash
cd SajorIPT/SajorWPF
dotnet build
```

### Run
```bash
dotnet run
```

Or open the solution in Visual Studio and run the project.

## NuGet Packages

The following packages are used:

- `Microsoft.EntityFrameworkCore` (9.0.0)
- `Microsoft.EntityFrameworkCore.SqlServer` (9.0.0)
- `Microsoft.Extensions.Configuration` (9.0.0)
- `Microsoft.Extensions.Configuration.Json` (9.0.0)
- `Microsoft.Extensions.Configuration.Binder` (9.0.0)
- `Microsoft.Extensions.DependencyInjection` (9.0.0)
- `Microsoft.Extensions.Hosting` (9.0.0)

## Extending the Application

### Adding a New Model

1. Create a new class in the `Models` folder
2. Add a `DbSet<YourModel>` to `AppDbContext`
3. Create migrations: `dotnet ef migrations add AddYourModel`
4. Update database: `dotnet ef database update`

### Adding a New Repository

1. Create an interface in `Repositories` folder (e.g., `IYourModelRepository`)
2. Implement the interface (e.g., `YourModelRepository`)
3. Register in `App.xaml.cs`:
   ```csharp
   services.AddScoped<IYourModelRepository, YourModelRepository>();
   ```

### Adding a New ViewModel

1. Create a class inheriting from `BaseViewModel`
2. Inject required repositories via constructor
3. Register in `App.xaml.cs`:
   ```csharp
   services.AddTransient<YourViewModel>();
   ```

### Adding a New View

1. Create a new Window/UserControl in the `Views` folder
2. Update the code-behind to accept the ViewModel via constructor
3. Set the `DataContext` in the constructor
4. Register in `App.xaml.cs`:
   ```csharp
   services.AddTransient<YourView>();
   ```

## MVVM Best Practices Implemented

1. **Separation of Concerns**: Clear boundaries between Models, Views, and ViewModels
2. **Dependency Injection**: Loose coupling through constructor injection
3. **Repository Pattern**: Data access logic abstracted from ViewModels
4. **INotifyPropertyChanged**: Automatic UI updates via property change notifications
5. **Observable Collections**: Automatic UI updates for collection changes
6. **Configuration Management**: Externalized configuration in appsettings.json

## Troubleshooting

### Database Connection Issues

If you encounter database connection errors:
1. Verify SQL Server is running
2. Check the connection string in `appsettings.json`
3. Ensure the database exists (run migrations)

### Build Issues

If the build fails:
1. Ensure .NET 9.0 SDK is installed
2. Run `dotnet restore` to restore NuGet packages
3. Clean and rebuild: `dotnet clean && dotnet build`

## Next Steps

Consider adding:
- Command pattern for user actions
- Navigation service for multi-window applications
- Validation using IDataErrorInfo or FluentValidation
- Async command support
- Unit tests for ViewModels and Repositories
- Message bus for component communication

## License

This is a learning project for IPT101 course.
