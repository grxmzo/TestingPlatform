using System.Text.Json.Serialization;
using TestingPlatform.Enums;

namespace TestingPlatform.Models;

public class Test
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsRepeatable { get; set; }
    public TestType Type { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset PublishedAt { get; set; }
    public DateTimeOffset Deadline { get; set; }
    public int? DurationMinutes { get; set; }
    public bool IsPublic { get; set; }
    public int? PassingScore { get; set; }
    public int? MaxAttempts { get; set; }

    [JsonIgnore]
    public List<Question> Questions { get; set; } = new();
    [JsonIgnore]
    public List<Student> Students { get; set; } = new();
    [JsonIgnore]
    public List<Project> Projects { get; set; } = new();
    [JsonIgnore]
    public List<Course> Courses { get; set; } = new();
    [JsonIgnore]
    public List<Group> Groups { get; set; } = new();
    [JsonIgnore]
    public List<Direction> Directions { get; set; } = new();
}