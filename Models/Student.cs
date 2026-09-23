namespace TestingPlatform.Models;

public class Student
{
    public int Id { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string VkProfileLink { get; set; } = string.Empty;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}