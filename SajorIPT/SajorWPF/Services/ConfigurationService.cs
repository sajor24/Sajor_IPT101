using Microsoft.Extensions.Configuration;

namespace SajorWPF.Services
{
    public class ConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public string GetConnectionString(string name)
        {
            return _configuration.GetConnectionString(name) ?? string.Empty;
        }

        public IConfiguration Configuration => _configuration;
    }
}
