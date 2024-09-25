using FreshFarm.Application.Mapper;
using FreshFarm.Application.Services.Auth;
using FreshFarm.Application.Services.Auth.Settings;
using FreshFarm.Application.Services.User;
using FreshFarm.Domain.Service.Auth;
using FreshFarm.Domain.Service.User;
using FreshFarm.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreshFarm.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,  ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings(
            configuration.GetValue<int>($"{JwtSettings.SectionName}:ExpiryMinutes"),
            configuration.GetValue<string>($"{JwtSettings.SectionName}:Issuer"),
            configuration.GetValue<string>($"{JwtSettings.SectionName}:Audience")
        );
        services.AddSingleton(jwtSettings);


        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddScoped<UserMapper>();

        services.AddInfrastructure(configuration);

        return services;
    }
}
