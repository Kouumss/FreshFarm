namespace  FreshFarm.Domain.Dtos.User;

public record UserDto
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
}
