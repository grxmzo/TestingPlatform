namespace TestingPlatform.Requests.Group;

public class UpdateGroupRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DirectionId { get; set; }
    public int CourseId { get; set; }
    public int ProjectId { get; set; }
}