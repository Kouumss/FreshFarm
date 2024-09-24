using FreshFarm.Domain.Entities;

namespace FreshFarm.Contract.Service.User;


public interface IUserRepository
{
    Task<UserEntity> AddAsync(UserEntity user);
    Task<UserEntity?> GetByIdAsync(Guid userId);
    Task<UserEntity?> GetByEmailAsync(string email);
    Task<IReadOnlyList<UserEntity>> GetAllAsync();
    Task UpdateAsync(UserEntity user);
    Task RemoveAsync(Guid userId);
}
