using AutoMapper;
using TestAudisoft.Application.Abstractions.Persistence;

namespace TestAudisoft.Application.Student.UseCases.StudentBusiness
{
    public class StudentBusiness : IStudentBusiness
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentBusiness(IMapper mapper, IStudentRepository studentRepository)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }
    }
}
