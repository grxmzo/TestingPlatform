namespace TestingPlatform.Models;

public class TestDirection
{
    public int TestId { get; set; }
    public Test Test { get; set; } = null!;
    public int DirectionId { get; set; }
    public Direction Direction { get; set; } = null!;
}