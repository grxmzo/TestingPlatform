using System.Text.Json.Serialization;

namespace TestingPlatform.Models;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int DirectionId { get; set; }
    public Direction? Direction { get; set; }

    public int CourseId { get; set; }
    public Course? Course { get; set; }

    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    [JsonIgnore]
    public List<Student> Students { get; set; } = new();
    [JsonIgnore]
    public List<Test> Tests { get; set; } = new();
}