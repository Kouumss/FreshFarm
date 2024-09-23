using FreshFarm.Application.Error;
using FreshFarm.Contract.DTOs.Auth;
using FreshFarm.Contract.Service.Auth;
using FreshFarm.Domain.Entities;

namespace FreshFarm.Application.Services.Auth;

public class AuthService : IAuthService
{

    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordService _passwordService;
    private readonly IAuthRepository _repository;


      public AuthService(IJwtTokenService jwtTokenService, IAuthRepository authRepository, IPasswordService passwordService)
    {
        _jwtTokenService = jwtTokenService;
        _repository = authRepository;
        _passwordService = passwordService;
    }

    public async Task<AuthResult> Login(LoginRequest request)
    {
        // Check if the user exists.
        var user = await _repository.GetUserByEmailAsync(request.Email) ?? throw new LoginErrorException();
       

        // Check if password is correct.
        if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new PasswordErrorException();
        }
        // Create JWT token.
        var token = await _jwtTokenService.GenerateTokenAsync(user);

        return new AuthResult(user, token);
    }

    public async Task<UserEntity> Register(RegisterRequest request)
    {
         // Set Password 
        _passwordService.CreateHashPassword(request.Password, out byte[] passwordHash, out byte[] passwordSelt);

        // Create user 

        var result = UserEntity.Create(request.FirstName, request.LastName, request.Email, passwordHash, passwordSelt);

        await _repository.AddAsync(result);

        return result;
    }
}
