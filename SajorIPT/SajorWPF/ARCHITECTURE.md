# MVVM Architecture Flow - Quick Reference

## Application Startup Flow

```
App.xaml.cs (OnStartup)
    ↓
1. Load Configuration (appsettings.json)
    ↓
2. Configure Services (DI Container)
    ├── DbContext (AppDbContext)
    ├── Repositories (IPersonRepository → PersonRepository)
    ├── ViewModels (MainViewModel)
    └── Views (MainWindow)
    ↓
3. Resolve MainWindow from DI
    ↓
4. MainWindow.Show()
```

## Dependency Injection Registration

```csharp
// In App.xaml.cs → ConfigureServices()

services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

services.AddScoped<IPersonRepository, PersonRepository>();
services.AddTransient<MainViewModel>();
services.AddTransient<MainWindow>();
```

## Data Flow - Loading People

```
MainViewModel Constructor
    ↓
Inject IPersonRepository
    ↓
LoadPeopleAsync() called
    ↓
PersonRepository.GetAllAsync()
    ↓
AppDbContext.People.ToListAsync()
    ↓
SQL Server Database Query
    ↓
Return List<Person>
    ↓
Create ObservableCollection<Person>
    ↓
Set People Property (triggers INotifyPropertyChanged)
    ↓
MainWindow.xaml ListBox updates automatically
```

## Key Classes and Their Responsibilities

### Models Layer
- **Person.cs**: Data entity (POCO)
  - Properties: Id, FirstName, LastName, CreatedAt

### Data Layer
- **AppDbContext.cs**: EF Core DbContext
  - DbSet<Person> People

### Repository Layer
- **IPersonRepository.cs**: Repository interface
  - GetAllAsync(), GetByIdAsync(), AddAsync(), UpdateAsync(), DeleteAsync()
- **PersonRepository.cs**: Repository implementation
  - Uses AppDbContext for data access

### ViewModel Layer
- **BaseViewModel.cs**: Base class with INotifyPropertyChanged
  - OnPropertyChanged() method
  - SetProperty() helper method
- **MainViewModel.cs**: Main window ViewModel
  - People: ObservableCollection<Person>
  - LoadPeopleAsync() method

### View Layer
- **MainWindow.xaml**: UI definition
  - ListBox with ItemsSource="{Binding People}"
- **MainWindow.xaml.cs**: Code-behind
  - Constructor accepts MainViewModel
  - Sets DataContext to ViewModel

## Configuration Files

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SajorWPFDb;..."
  }
}
```

### SajorWPF.csproj (Key Parts)
```xml
<PropertyGroup>
  <TargetFramework>net9.0-windows</TargetFramework>
  <UseWPF>true</UseWPF>
</PropertyGroup>

<ItemGroup>
  <!-- EF Core packages -->
  <PackageReference Include="Microsoft.EntityFrameworkCore" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" />
  
  <!-- Configuration packages -->
  <PackageReference Include="Microsoft.Extensions.Configuration" />
  <PackageReference Include="Microsoft.Extensions.Configuration.Json" />
  
  <!-- DI packages -->
  <PackageReference Include="Microsoft.Extensions.DependencyInjection" />
</ItemGroup>

<ItemGroup>
  <None Update="appsettings.json">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

## MVVM Pattern Benefits

✅ **Separation of Concerns**: Clear boundaries between UI, logic, and data
✅ **Testability**: ViewModels can be unit tested without UI
✅ **Maintainability**: Changes to UI don't affect business logic
✅ **Data Binding**: Automatic UI updates via INotifyPropertyChanged
✅ **Dependency Injection**: Loose coupling, easy to mock dependencies
✅ **Repository Pattern**: Data access abstraction

## Quick Commands

### Build
```bash
dotnet build
```

### Run
```bash
dotnet run
```

### Create Migration
```bash
dotnet ef migrations add MigrationName
```

### Update Database
```bash
dotnet ef database update
```

### Add EF Core Tools
```bash
dotnet tool install --global dotnet-ef
```
