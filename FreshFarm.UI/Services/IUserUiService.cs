using FreshFarm.Domain.Dtos.User;
using FreshFarm.UI.Models.User;

namespace FreshFarm.UI.Services;

public interface IUserUiService
{   
    Task<IReadOnlyList<UserViewModel>> GetAllUsersAsync();
    Task<UserViewModel?> GetUserByIdAsync(Guid userId);
    Task UpdateUserAsync(Guid userId, UserUpdateDto userUpdateDto);
    Task DeleteUserAsync(Guid userId);
}
