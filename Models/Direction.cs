using System.Text.Json.Serialization;

namespace TestingPlatform.Models;

public class Direction
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public List<Group> Groups { get; set; } = new();
}