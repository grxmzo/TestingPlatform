using System.ComponentModel.DataAnnotations;

namespace TestingPlatform.Requests.Answer;

public class CreateAnswerRequest
{
    [Required] public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public int QuestionId { get; set; }
}