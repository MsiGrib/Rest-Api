using Microsoft.EntityFrameworkCore;

namespace EntityGateWay
{
    public static class ConfigurationMigrator
    {
        public static void Applay(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BonchiDBContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using var context = new BonchiDBContext(optionsBuilder.Options);
            context.Database.Migrate();
        }
    }
}