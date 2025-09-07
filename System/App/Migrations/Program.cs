using EntityGateWay;
using Microsoft.Extensions.Configuration;

namespace Migrations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetSection("Application")["ConnectionString"];

            ConfigurationMigrator.Applay(connectionString!);

            Console.WriteLine("Миграции применены!");
        }
    }
}