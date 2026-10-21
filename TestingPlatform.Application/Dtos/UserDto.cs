using TestingPlatform.Domain.Enums;

namespace TestingPlatform.Application.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string Password { get; set; } = string.Empty;
}