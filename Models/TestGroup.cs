namespace TestingPlatform.Models;

public class TestGroup
{
    public int TestId { get; set; }
    public Test Test { get; set; } = null!;
    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;
}