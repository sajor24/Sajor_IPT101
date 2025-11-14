using System.IO;
using Microsoft.Extensions.Configuration;

namespace SajorWPF.Services
{
    public class ConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public string GetConnectionString(string name)
        {
            return _configuration.GetConnectionString(name) ?? string.Empty;
        }
    }
}
