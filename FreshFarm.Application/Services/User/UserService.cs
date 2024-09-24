using FreshFarm.Application.Error;
using FreshFarm.Application.Mapper;
using FreshFarm.Contract.DTOs.User;
using FreshFarm.Contract.Service.Auth;
using FreshFarm.Contract.Service.User;
using FreshFarm.Domain.Entities;

namespace FreshFarm.Application.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly UserMapper _userMapper;


    public UserService(IUserRepository userRepository, IPasswordService passwordService, UserMapper userMapper)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _userMapper = userMapper;
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        // Vérifier si l'email existe déjà
        UserEntity? existingUser = await _userRepository.GetByEmailAsync(createUserDto.Email);
        if (existingUser is not null)
            throw new InvalidOperationException("Un utilisateur avec cet email existe déjà.");

        // Hachage du mot de passe
        _passwordService.CreateHashPassword(createUserDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        // Mapper le DTO vers l'entité
        UserEntity user = UserEntity.Create(
            createUserDto.FirstName,
            createUserDto.LastName,
            createUserDto.Email,
            passwordHash,
            passwordSalt
        );

        UserEntity createdUser = await _userRepository.AddAsync(user);
        return _userMapper.MapToDto(createdUser);
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new NotFoundException("Utilisateur non trouvé.");
        return _userMapper.MapToDto(user);
    }


    public async Task<IReadOnlyList<UserDto>> GetAllUsersAsync()
    {
        IReadOnlyList<UserEntity> users = await _userRepository.GetAllAsync() ?? new List<UserEntity>();

        // Map every UserEntity to UserDto
        var userDtos = users.Select(_userMapper.MapToDto).ToList();

        return userDtos;
    }

    public async Task UpdateUserAsync(Guid userId, UpdateUserDto updateUserDto)
    {
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new NotFoundException("Utilisateur non trouvé.");

        string firstName = updateUserDto.FirstName ?? user.FirstName;
        string lastName = updateUserDto.LastName ?? user.LastName;
        string email = updateUserDto.Email ?? user.Email;

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

    private UserDto MapToDto(UserEntity user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            // Mapper d'autres propriétés si nécessaire
        };
    }
}

