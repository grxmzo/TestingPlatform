using AutoMapper;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Requests.Student;
using TestingPlatform.Responses.Student;

namespace TestingPlatform.Mappings;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<StudentDto, StudentResponse>()
            .ForMember(d => d.Login, m => m.MapFrom(s => s.User.Login))
            .ForMember(d => d.Email, m => m.MapFrom(s => s.User.Email))
            .ForMember(d => d.FirstName, m => m.MapFrom(s => s.User.FirstName))
            .ForMember(d => d.MiddleName, m => m.MapFrom(s => s.User.MiddleName))
            .ForMember(d => d.LastName, m => m.MapFrom(s => s.User.LastName));

        CreateMap<UpdateStudentRequest, StudentDto>();
    }
}