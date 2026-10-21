using System.ComponentModel.DataAnnotations;
using TestingPlatform.Domain.Enums;

namespace TestingPlatform.Requests.User;

public class CreateUserRequest
{
    [Required] public string Login { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required] public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    [Required] public string LastName { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}