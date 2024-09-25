using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FreshFarm.Infrastructure.Persistence.DbContexts
{
    public class DbContextFactory : IDesignTimeDbContextFactory<FreshFarmDbContext>
    {
        public FreshFarmDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FreshFarmDbContext>();

            // Charger la configuration depuis le fichier appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("FreshFarm");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("La chaîne de connexion 'FreshFarm' est introuvable dans le fichier de configuration.");
            }

            builder.UseNpgsql(connectionString);

            return new FreshFarmDbContext(builder.Options);
        }
    }
}
