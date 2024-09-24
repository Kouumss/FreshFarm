using System;
using FreshFarm.Contract.DTOs.User;
using FreshFarm.Domain.Entities;

namespace FreshFarm.Contract.Service.User;
public interface IUserService
{
    Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
    Task<UserDto?> GetUserByIdAsync(Guid userId);
    Task<IReadOnlyList<UserDto>> GetAllUsersAsync();
    Task UpdateUserAsync(Guid userId, UpdateUserDto updateUserDto);
    Task RemoveUserAsync(Guid userId);
}

