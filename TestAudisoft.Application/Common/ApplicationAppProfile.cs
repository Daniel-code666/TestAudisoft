using AutoMapper;
using TestAudisoft.Application.Grades.Dtos;
using TestAudisoft.Application.Professor.Dtos;
using TestAudisoft.Application.Student.Dtos;
using TestAudisoft.Entities;

namespace TestAudisoft.Application.Common
{
    public class ApplicationAppProfile : Profile
    {
        public ApplicationAppProfile()
        {
            MapStudent();
            MapProfessor();
            MapGrades();
        }

        private void MapStudent()
        {
            CreateMap<StudentEntity, StudentDto>();

            CreateMap<StudentCreateDto, StudentEntity>();

            CreateMap<StudentUpdateDto, StudentEntity>();

            CreateMap<StudentEntity, StudentWithGradesDto>();

            CreateMap<GradesEntity, StudentGradeDetailDto>()
                .ForMember(dest => dest.GradeValue, opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.ProfessorId, opt => opt.MapFrom(src => src.ProfessorId))
                .ForMember(dest => dest.ProfessorFirstName, opt => opt.MapFrom(src => src.Professor.FirstName))
                .ForMember(dest => dest.ProfessorLastName, opt => opt.MapFrom(src => src.Professor.LastName))
                .ForMember(dest => dest.ProfessorEmail, opt => opt.MapFrom(src => src.Professor.Email));
        }

        private void MapProfessor()
        {
            CreateMap<ProfessorEntity, ProfessorDto>();

            CreateMap<ProfessorCreateDto, ProfessorEntity>();

            CreateMap<ProfessorUpdateDto, ProfessorEntity>();

            CreateMap<ProfessorEntity, ProfessorWithGradesDto>();

            CreateMap<GradesEntity, ProfessorGradeDetailDto>()
                .ForMember(dest => dest.GradeValue, opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.Student.FirstName))
                .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.Student.LastName))
                .ForMember(dest => dest.StudentEmail, opt => opt.MapFrom(src => src.Student.Email));

        }

        private void MapGrades()
        {
            CreateMap<GradesEntity, GradeDto>()
                .ForMember(dest => dest.GradeValue, opt => opt.MapFrom(src => src.Grade));

            CreateMap<GradeCreateDto, GradesEntity>()
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.GradeValue));

            CreateMap<GradeUpdateDto, GradesEntity>()
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.GradeValue));

        }
    }
}
