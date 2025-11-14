using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SajorWPF.Data;
using SajorWPF.Repositories;
using SajorWPF.Services;
using SajorWPF.ViewModels;

namespace SajorWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider? _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            // Initialize database
            InitializeDatabase();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            // Configuration
            var configService = new ConfigurationService();
            services.AddSingleton(configService);

            // Database
            var connectionString = configService.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Repositories
            services.AddScoped<IPersonRepository, PersonRepository>();

            // ViewModels
            services.AddTransient<PersonViewModel>();

            // Windows
            services.AddTransient<MainWindow>();
        }

        private void InitializeDatabase()
        {
            using var scope = _serviceProvider!.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // Note: For production, consider using migrations (dotnet ef migrations add/update)
            // EnsureCreated() is used here for simplicity in this minimal MVVM demo
            dbContext.Database.EnsureCreated();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider?.Dispose();
            base.OnExit(e);
        }
    }

}
