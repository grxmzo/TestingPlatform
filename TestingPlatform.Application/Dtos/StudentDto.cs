namespace TestingPlatform.Application.Dtos;

public class StudentDto
{
    public int Id { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string VkProfileLink { get; set; } = string.Empty;
    public int UserId { get; set; }
    public UserDto User { get; set; } = null!;
    public int? EducationScore { get; set; }
    public int? AdditionalActivityScore { get; set; }
    public int? OtherScore { get; set; }
}