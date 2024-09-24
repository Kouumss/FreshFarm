using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreshFarm.Infrastructure.Persistence.DbContexts
{
    public class DbContextFactory : IDesignTimeDbContextFactory<FreshFarmDbContext>
    {
        private readonly IConfiguration _configuration;

        public DbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FreshFarmDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FreshFarmDbContext>();

            var connectionString = _configuration.GetConnectionString("Default");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("La chaîne de connexion 'Default' est introuvable dans le fichier de configuration.");
            }

            builder.UseNpgsql(connectionString);

            return new FreshFarmDbContext(builder.Options);
        }
    }
}
