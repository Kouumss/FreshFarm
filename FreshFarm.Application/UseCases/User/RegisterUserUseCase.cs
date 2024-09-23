using System;
using FreshFarm.Application.Services.Auth;
using FreshFarm.Contract.DTOs.Auth;
using FreshFarm.Domain.Entities;

namespace FreshFarm.Application.UseCases.User;

public class RegisterUserUseCase
{
    private readonly AuthService _authService;

    public RegisterUserUseCase(AuthService authService)
    {
        _authService = authService;
    }

    public async Task<UserEntity> Execute(RegisterRequest request)
    {
        return await _authService.Register(request);
    }
}