namespace TestingPlatform.Models;

public class UserTextAnswer
{
    public int Id { get; set; }
    public string TextAnswer { get; set; } = string.Empty;
    public int UserAttemptAnswerId { get; set; }
    public UserAttemptAnswer UserAttemptAnswer { get; set; } = null!;
}