using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces;

public interface IGroupRepository
{
    List<GroupDto> GetAll();
    GroupDto GetById(int id);
    int Create(GroupDto groupDto);
    void Update(GroupDto groupDto);
    void Delete(int id);
}