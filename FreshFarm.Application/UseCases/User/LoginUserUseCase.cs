using System;
using FreshFarm.Application.Services.Auth;
using FreshFarm.Contract.DTOs.Auth;

namespace FreshFarm.Application.UseCases.User;

public class LoginUserUseCase
{
    private readonly AuthService _authService;

    public LoginUserUseCase(AuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResult> Execute(LoginRequest request)
    {
        return await _authService.Login(request);
    }
}