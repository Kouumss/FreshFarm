namespace FreshFarm.Domain.Dtos.Auth;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);