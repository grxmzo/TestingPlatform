using System.ComponentModel.DataAnnotations;

namespace TestingPlatform.Requests.Student;

public class CreateStudentRequest
{
    [Required] public string Login { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required] public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    [Required] public string LastName { get; set; } = string.Empty;
    [Required] public string Phone { get; set; } = string.Empty;
    [Required] public string VkProfileLink { get; set; } = string.Empty;
}