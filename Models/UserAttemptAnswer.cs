using System.Text.Json.Serialization;

namespace TestingPlatform.Models;

public class UserAttemptAnswer
{
    public int Id { get; set; }
    public bool IsCorrect { get; set; }
    public int ScoreAwarded { get; set; }
    public int AttemptId { get; set; }
    public Attempt Attempt { get; set; } = null!;
    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;

    [JsonIgnore]
    public List<UserSelectedOption> UserSelectedOptions { get; set; } = new();
    [JsonIgnore]
    public UserTextAnswer? UserTextAnswer { get; set; }
}