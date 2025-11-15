using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using SajorWPF.Data;
using SajorWPF.ViewModels;

namespace SajorWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost? _host;
        private IServiceScope? _scope;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Register DbContext with SQL Server
                    services.AddDbContext<EmployeeContext>(options =>
                        options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

                    // Register ViewModels and Views
                    services.AddScoped<MainViewModel>();
                    services.AddScoped<MainWindow>();
                })
                .Build();

            await _host.StartAsync();

            // Create a scope and resolve MainWindow
            _scope = _host.Services.CreateScope();
            var mainWindow = _scope.ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            _scope?.Dispose();
            
            if (_host != null)
            {
                await _host.StopAsync();
                _host.Dispose();
            }

            base.OnExit(e);
        }
    }
}
