using System.Text.Json.Serialization;

namespace TestingPlatform.Models;

public class Student
{
    public int Id { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string VkProfileLink { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [JsonIgnore]
    public List<Group> Groups { get; set; } = new();
    [JsonIgnore]
    public List<Test> Tests { get; set; } = new();
    [JsonIgnore]
    public List<Attempt> Attempts { get; set; } = new();
    [JsonIgnore]
    public List<TestResult> TestResults { get; set; } = new();
}