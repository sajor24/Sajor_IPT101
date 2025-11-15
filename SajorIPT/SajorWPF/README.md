# MVVM + EF Core Employee Manager Implementation

This document describes the MVVM + EF Core implementation for the Employee Manager application.

## Overview

This implementation follows the MVVM (Model-View-ViewModel) architectural pattern and uses Entity Framework Core with SQL Server for data persistence. The application uses Microsoft Generic Host for dependency injection.

## Architecture

### Models
- **Employee.cs**: Entity class with properties: Id, FirstName, LastName, Age, Position

### Data Layer
- **EmployeeContext.cs**: DbContext class with DbSet<Employee>
- **DesignTimeDbContextFactory.cs**: Factory for EF Core design-time tools to create DbContext

### ViewModels
- **MainViewModel.cs**: Implements INotifyPropertyChanged with:
  - ObservableCollection<Employee> for data binding
  - Commands: Add, Update, Delete, Clear
  - Properties for form fields and selected employee

### Views
- **MainWindow.xaml**: WPF window with data bindings to MainViewModel
- **MainWindow.xaml.cs**: Code-behind that accepts MainViewModel via DI

### Helpers & Converters
- **RelayCommand.cs**: Simple ICommand implementation for commands
- **PlaceholderConverter.cs**: Value converter for TextBox placeholder functionality

### Configuration
- **App.xaml.cs**: Configures Generic Host with:
  - DbContext registration with SQL Server
  - Scoped services for ViewModel and Window
  - Proper disposal on application exit

- **appsettings.json**: Configuration file with connection string
- **SajorWPF.csproj**: Project file with EF Core packages and appsettings.json copy configuration

## Database Setup

After pulling this code, run the following commands to set up the database:

```bash
cd SajorIPT/SajorWPF
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## NuGet Packages Added

- Microsoft.EntityFrameworkCore (9.0.0)
- Microsoft.EntityFrameworkCore.SqlServer (9.0.0)
- Microsoft.EntityFrameworkCore.Tools (9.0.0)
- Microsoft.EntityFrameworkCore.Design (9.0.0)
- Microsoft.Extensions.Hosting (9.0.0)
- Microsoft.Extensions.Configuration.Json (9.0.0)

## Connection String

The default connection string points to SQL Server LocalDB:
```
Server=(localdb)\\MSSQLLocalDB;Database=SajorEmployeeDb;Trusted_Connection=True;
```

This can be modified in appsettings.json to point to any SQL Server instance.

## Features

- Add new employees
- Update existing employees
- Delete employees (with confirmation)
- Clear form fields
- View all employees in a DataGrid
- Full CRUD operations with Entity Framework Core
- Dependency injection throughout the application
- Proper resource disposal
