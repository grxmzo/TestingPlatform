namespace TestingPlatform.Models;

public class UserSelectedOption
{
    public int Id { get; set; }
    public int UserAttemptAnswerId { get; set; }
    public UserAttemptAnswer UserAttemptAnswer { get; set; } = null!;
    public int AnswerId { get; set; }
    public Answer Answer { get; set; } = null!;
}