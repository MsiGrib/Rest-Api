using Microsoft.Extensions.DependencyInjection;
using SMTP.Services.Implementations;
using SMTP.Services.Interfaces;

namespace SMTP
{
    public static class Configuration
    {
        public static void ConfigurationSMTP(IServiceCollection collection)
        {
            collection.AddScoped<ISMTPService, SMTPService>();
        }
    }
}