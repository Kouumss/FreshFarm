using FreshFarm.Domain.Entities;

namespace FreshFarm.Domain.Dtos.Auth;

public record AuthResult(UserEntity User, string Token);
