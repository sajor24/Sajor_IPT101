using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajorIPT101Solution.SajorFramework;

namespace SajorIPT101Solution.SajorYouTubeViewer.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString("connect");

                services.AddSingleton<DbContextOptions>(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options);
                services.AddSingleton<YouTubeViewersDbContextFactory>();
            });
                
            return hostBuilder;
        }
    }
}
