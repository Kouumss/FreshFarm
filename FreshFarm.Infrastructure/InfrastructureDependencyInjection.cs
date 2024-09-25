using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FreshFarm.Domain.Service.User;
using FreshFarm.Infrastructure.Persistence.Repositories.User;
using FreshFarm.Domain.Service.Auth;
using FreshFarm.Infrastructure.Persistence.Repositories.Auth;
using FreshFarm.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FreshFarm.Infrastructure;
public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();

        services.AddDbContext<FreshFarmDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("FreshFarm")));

        return services;
    }
}

