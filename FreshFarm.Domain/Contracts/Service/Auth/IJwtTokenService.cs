using FreshFarm.Domain.Entities;

namespace FreshFarm.Domain.Service.Auth;

public interface IJwtTokenService
{
    Task<string> GenerateTokenAsync(UserEntity user);
}