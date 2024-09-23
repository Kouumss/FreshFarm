using FreshFarm.Domain.Entities;

namespace FreshFarm.Contract.DTOs.Auth;

public record AuthResult(UserEntity User, string Token);
