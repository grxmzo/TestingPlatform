using System.ComponentModel.DataAnnotations;
using TestingPlatform.Domain.Enums;

namespace TestingPlatform.Requests.Test;

public class UpdateTestRequest
{
    public int Id { get; set; }
    [Required] public string Title { get; set; } = string.Empty;
    [Required] public string Description { get; set; } = string.Empty;
    public bool IsRepeatable { get; set; }
    public TestType Type { get; set; }
    public DateTime PublishedAt { get; set; } // БЫЛО DateTimeOffset
    public DateTime Deadline { get; set; }    // БЫЛО DateTimeOffset
    public int? DurationMinutes { get; set; }
    public bool IsPublic { get; set; }
    public int? PassingScore { get; set; }
    public int? MaxAttempts { get; set; }
}