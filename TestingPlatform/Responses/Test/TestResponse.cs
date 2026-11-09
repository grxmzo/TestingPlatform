using TestingPlatform.Domain.Enums;

namespace TestingPlatform.Responses.Test;

public class TestResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsRepeatable { get; set; }
    public TestType Type { get; set; }
    public DateTime CreatedAt { get; set; }   // БЫЛО DateTimeOffset
    public DateTime PublishedAt { get; set; } // БЫЛО DateTimeOffset
    public DateTime Deadline { get; set; }    // БЫЛО DateTimeOffset
    public int? DurationMinutes { get; set; }
    public bool IsPublic { get; set; }
    public int? PassingScore { get; set; }
    public int? MaxAttempts { get; set; }
}