using TestingPlatform.Responses.Course;
using TestingPlatform.Responses.Direction;
using TestingPlatform.Responses.Project;

namespace TestingPlatform.Responses.Group;

public class GroupResponse : BaseResponse
{
    public DirectionResponse Direction { get; set; } = null!;
    public CourseResponse Course { get; set; } = null!;
    public ProjectResponse Project { get; set; } = null!;
}