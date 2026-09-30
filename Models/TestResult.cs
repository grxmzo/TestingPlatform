namespace TestingPlatform.Models;

public class TestResult
{
    public int Id { get; set; }
    public bool Passed { get; set; }
    public int TestId { get; set; }
    public Test Test { get; set; } = null!;
    public int AttemptId { get; set; }
    public Attempt Attempt { get; set; } = null!;
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
}