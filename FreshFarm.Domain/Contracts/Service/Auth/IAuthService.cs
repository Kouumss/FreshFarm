using FreshFarm.Domain.Dtos.Auth;
using FreshFarm.Domain.Entities;

namespace FreshFarm.Domain.Service.Auth;

public interface IAuthService
{
    Task<AuthResult> Login(LoginRequest request);
    Task<UserEntity> Register(RegisterRequest request);
}
