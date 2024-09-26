using FreshFarm.Application.Error;
using FreshFarm.Domain.Dtos.User;
using FreshFarm.Domain.Service.Auth;
using FreshFarm.Domain.Service.User;
using FreshFarm.Domain.Entities;

namespace FreshFarm.Application.Services.User;

public class UserService : IUserService
{
    #region Services
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    #endregion

    #region Constructor 
    public UserService(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }
    #endregion

    #region Method

    // GET ALL
    public async Task<IReadOnlyList<UserEntity>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    // GET BY ID
    public async Task<UserEntity?> GetUserByIdAsync(Guid userId)
    {
        return await _userRepository.GetByIdAsync(userId) ?? throw new NotFoundException("Utilisateur non trouvé.");
    }

    // CREATE
    public async Task<UserEntity> CreateUserAsync(UserCreateDto model)
    {
        UserEntity? existingUser = await _userRepository.GetByEmailAsync(model.Email);
        if (existingUser is not null)
            throw new InvalidOperationException("Un utilisateur avec cet email existe déjà.");

        _passwordService.CreateHashPassword(
            model.Password,
            out byte[] passwordHash,
            out byte[] passwordSalt
        );

        var user = UserEntity.Create(
            model.FirstName,
            model.LastName,
            model.Email,
            passwordHash,
            passwordSalt
        );

        return await _userRepository.AddAsync(user);
    }

    // UPDATE
    public async Task UpdateUserAsync(Guid userId, UserUpdateDto model)
    {
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new NotFoundException("Utilisateur non trouvé.");

        string firstName = model.FirstName ?? user.FirstName;
        string lastName = model.LastName ?? user.LastName;
        string email = model.Email ?? user.Email;

        user.UpdateUserDetails(firstName, lastName, email);

        await _userRepository.UpdateAsync(user);
    }
    // DELETE
    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new NotFoundException("Utilisateur non trouvé.");
        await _userRepository.RemoveAsync(userId);
    }

    #endregion
}
