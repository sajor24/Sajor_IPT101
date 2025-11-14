using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SajorIPT101Solution.SajorDomain.Commands;
using SajorIPT101Solution.SajorDomain.Queries;
using SajorIPT101Solution.SajorFramework;
using SajorIPT101Solution.SajorFramework.Commands;
using SajorIPT101Solution.SajorFramework.Queries;
using SajorIPT101.SajorWPF.Stores;
using SajorIPT101.SajorWPF.ViewModels;
using SajorIPT101.SajorWPF.HostBuilders;
using Microsoft.EntityFrameworkCore;

namespace SajorIPT101.SajorWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddDbContext()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IGetAllEmployeesQuery, GetAllEmployeesQuery>();
                    services.AddSingleton<ICreateEmployeeCommand, CreateEmployeeCommand>();
                    services.AddSingleton<IUpdateEmployeeCommand, UpdateEmployeeCommand>();
                    services.AddSingleton<IDeleteEmployeeCommand, DeleteEmployeeCommand>();

                    services.AddSingleton<ModalNavigationStore>();
                    services.AddSingleton<EmployeesStore>();
                    services.AddSingleton<SelectedEmployeeStore>();

                    services.AddTransient<EmployeesViewModel>(CreateEmployeesViewModel);
                    services.AddSingleton<MainViewModel>();

                    services.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            EmployeesDbContextFactory employeesDbContextFactory = 
                _host.Services.GetRequiredService<EmployeesDbContextFactory>();
            using(EmployeesDbContext context = employeesDbContextFactory.Create())
            {
                context.Database.Migrate();
            }

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }

        private EmployeesViewModel CreateEmployeesViewModel(IServiceProvider services)
        {
            return EmployeesViewModel.LoadViewModel(
                services.GetRequiredService<EmployeesStore>(),
                services.GetRequiredService<SelectedEmployeeStore>(),
                services.GetRequiredService<ModalNavigationStore>());
        }
    }

}
