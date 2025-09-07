using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityGateWay
{
    internal class BonchiDBContextFactory : IDesignTimeDbContextFactory<BonchiDBContext>
    {
        public BonchiDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetSection("Application")["ConnectionString"];

            var optionsBuilder = new DbContextOptionsBuilder<BonchiDBContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new BonchiDBContext(optionsBuilder.Options);
        }
    }
}