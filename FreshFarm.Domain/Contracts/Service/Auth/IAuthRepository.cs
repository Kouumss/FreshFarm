using FreshFarm.Domain.Entities;

namespace FreshFarm.Domain.Service.Auth;


public interface IAuthRepository
{
    Task AddAsync(UserEntity user);
    Task<UserEntity?> GetUserByEmailAsync(string email);
}