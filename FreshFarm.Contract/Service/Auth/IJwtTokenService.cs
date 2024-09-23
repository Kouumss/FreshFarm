using FreshFarm.Domain.Entities;

namespace FreshFarm.Contract.Service.Auth;

public interface IJwtTokenService
{
    Task<string> GenerateTokenAsync(UserEntity user);
}