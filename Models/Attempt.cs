using System.Text.Json.Serialization;

namespace TestingPlatform.Models;

public class Attempt
{
	public int Id { get; set; }
	public DateTimeOffset StartedAt { get; set; } = DateTimeOffset.UtcNow;
	public DateTimeOffset? SubmittedAt { get; set; }
	public int? Score { get; set; }
	public int TestId { get; set; }
	public Test Test { get; set; } = null!;
	public int StudentId { get; set; }
	public Student Student { get; set; } = null!;

	[JsonIgnore]
	public List<UserAttemptAnswer> UserAttemptAnswers { get; set; } = new();
}