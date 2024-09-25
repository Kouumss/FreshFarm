using FreshFarm.Domain.Dtos.User;
using FreshFarm.Domain.Service.User;
using FreshFarm.UI.Models.User;

namespace FreshFarm.UI.Services;

public class UserUiService : IUserUiService
{
    private readonly IUserService _userService;

    public UserUiService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserViewModel?> GetUserByIdAsync(Guid userId)
    {
        var userDto = await _userService.GetUserByIdAsync(userId);
        return userDto != null ? new UserViewModel
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email
        } : null;
    }

    public async Task UpdateUserAsync(Guid userId, UserUpdateDto userUpdateModel)
    {
        await _userService.UpdateUserAsync(userId, userUpdateModel);
    }

    public async Task<IReadOnlyList<UserViewModel>> GetAllUsersAsync()
    {
        var userDtos = await _userService.GetAllUsersAsync();
        return userDtos.Select(dto => new UserViewModel
        {   
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email
        }).ToList().AsReadOnly();
    }

    public Task DeleteUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}

