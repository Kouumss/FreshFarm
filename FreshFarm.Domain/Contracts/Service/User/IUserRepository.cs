using FreshFarm.Domain.Entities;

namespace FreshFarm.Domain.Service.User;


public interface IUserRepository
{
    Task<UserEntity> AddAsync(UserEntity user);
    Task<UserEntity?> GetByIdAsync(Guid userId);
    Task<UserEntity?> GetByEmailAsync(string email);
    Task<IReadOnlyList<UserEntity>> GetAllAsync();
    Task UpdateAsync(UserEntity user);
    Task RemoveAsync(Guid userId);
}
