using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FreshFarm.Infrastructure.Persistence.DbContexts;

 public class DbConnectionFactory : IDesignTimeDbContextFactory<FreshFarmDbContext>
    {
        public FreshFarmDbContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();

            var configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Settings", "appsettings.json");

            // Check if configuration exist
            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException("Le fichier de configuration 'appsettings.json' est introuvable.", configFilePath);
            }

            // Loading configuration
            try
            {
                configurationBuilder.AddJsonFile(configFilePath);
                IConfigurationRoot configurationRoot = configurationBuilder.Build();

                var builder = new DbContextOptionsBuilder<FreshFarmDbContext>();

                // retrieving the connection string
                var connectionString = configurationRoot.GetConnectionString("Default");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("La chaîne de connexion 'Default' est introuvable dans le fichier de configuration.");
                }

                builder.UseNpgsql(connectionString);

                return new FreshFarmDbContext(builder.Options);
            }
            catch (Exception ex)
            {
                // Handle the exception
                throw new InvalidOperationException("Erreur lors de la création du DbContext.", ex);
            }
        }
    }
