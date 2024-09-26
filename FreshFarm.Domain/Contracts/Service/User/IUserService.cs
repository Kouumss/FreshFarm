using FreshFarm.Domain.Dtos.User;
using FreshFarm.Domain.Entities;

namespace FreshFarm.Domain.Service.User;
public interface IUserService
{
    Task<UserEntity> CreateUserAsync(UserCreateDto model);
    Task<UserEntity?> GetUserByIdAsync(Guid userId);
    Task<IReadOnlyList<UserEntity>> GetAllUsersAsync();
    Task UpdateUserAsync(Guid userId, UserUpdateDto model);
    Task DeleteUserAsync(Guid userId);
}

