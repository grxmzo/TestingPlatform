using System.Text.Json.Serialization;
using TestingPlatform.Enums;

namespace TestingPlatform.Models;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public int Number { get; set; }
    public string? Description { get; set; }
    public AnswerType AnswerType { get; set; }
    public bool IsScoring { get; set; } = true;
    public int? MaxScore { get; set; }
    public int TestId { get; set; }
    public Test Test { get; set; } = null!;

    [JsonIgnore]
    public List<UserAttemptAnswer> UserAttemptAnswers { get; set; } = new();
    [JsonIgnore]
    public List<Answer> Answers { get; set; } = new();
}