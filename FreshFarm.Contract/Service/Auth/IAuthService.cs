using FreshFarm.Contract.DTOs.Auth;
using FreshFarm.Domain.Entities;

namespace FreshFarm.Contract.Service.Auth;

public interface IAuthService
{
    Task<AuthResult> Login(LoginRequest request);
    Task<UserEntity> Register(RegisterRequest request);
}
