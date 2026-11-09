using System.Text.Json.Serialization;

namespace TestingPlatform.Domain.Models;

public class Attempt
{
    public int Id { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? SubmittedAt { get; set; }
    public int? Score { get; set; }
    public int TestId { get; set; }
    public Test Test { get; set; } = null!;
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    [JsonIgnore]
    public List<UserAttemptAnswer> UserAttemptAnswers { get; set; } = new();
}