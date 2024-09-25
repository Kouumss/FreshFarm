using FreshFarm.Domain.Dtos.User;

namespace FreshFarm.Domain.Service.User;
public interface IUserService
{
    Task<UserDto> CreateUserAsync(UserCreateDto model);
    Task<UserDto?> GetUserByIdAsync(Guid userId);
    Task<IReadOnlyList<UserDto>> GetAllUsersAsync();
    Task UpdateUserAsync(Guid userId, UserUpdateDto model);
    Task RemoveUserAsync(Guid userId);
}

