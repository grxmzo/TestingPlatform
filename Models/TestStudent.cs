namespace TestingPlatform.Models;

public class TestStudent
{
    public int TestId { get; set; }
    public Test Test { get; set; } = null!;
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
}