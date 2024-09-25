using FreshFarm.Application.Error;
using FreshFarm.Application.Mapper;
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
    private readonly UserMapper _userMapper;

    #endregion

    #region Constructor 
    public UserService(IUserRepository userRepository, IPasswordService passwordService, UserMapper userMapper)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _userMapper = userMapper;
    }
    #endregion

    #region Method

    // GET ALL
    public async Task<IReadOnlyList<UserDto>> GetAllUsersAsync()
    {
        var result = await _userRepository.GetAllAsync() ?? Enumerable.Empty<UserEntity>();

        return result.Select(_userMapper.MapToDto).ToList().AsReadOnly();
    }
    // GET BY ID
    public async Task<UserDto?> GetUserByIdAsync(Guid userId)
{
    var userEntity = await _userRepository.GetByIdAsync(userId) ?? throw new NotFoundException("Utilisateur non trouvé.");

    UserDto userDto = _userMapper.MapToDto(userEntity);
    
    return userDto; 
}

    // CREATE
    public async Task<UserDto> CreateUserAsync(UserCreateDto model)
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

        UserEntity createdUser = await _userRepository.AddAsync(user);
        UserDto userViewModel = new UserDto();

        userViewModel.FirstName = createdUser.FirstName;
        userViewModel.LastName = createdUser.LastName;
        userViewModel.Email = createdUser.Email;

        return userViewModel;
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

    public async Task RemoveUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new NotFoundException("Utilisateur non trouvé.");

        await _userRepository.RemoveAsync(userId);
    }

    // PASSWORD
    public async Task UpdatePasswordAsync(Guid userId, UpdatePasswordDto updatePasswordDto)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
            throw new NotFoundException("Utilisateur non trouvé.");

        // Check validation of current password
        if (!_passwordService.VerifyPassword(updatePasswordDto.CurrentPassword, user.PasswordHash, user.PasswordSalt))
            throw new InvalidOperationException("Le mot de passe actuel est incorrect.");

        // Hash new password
        _passwordService.CreateHashPassword(updatePasswordDto.NewPassword, out byte[] newPasswordHash, out byte[] newPasswordSalt);

        user.UpdatePassword(newPasswordHash, newPasswordSalt);

        await _userRepository.UpdateAsync(user);
    }
    #endregion
}

