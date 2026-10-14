namespace TestingPlatform.Application.Dtos;

public class GroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DirectionDto Direction { get; set; } = null!;
    public CourseDto Course { get; set; } = null!;
    public ProjectDto Project { get; set; } = null!;
}