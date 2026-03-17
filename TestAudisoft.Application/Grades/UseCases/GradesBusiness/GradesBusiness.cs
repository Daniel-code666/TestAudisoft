using AutoMapper;
using TestAudisoft.Application.Abstractions.Persistence;

namespace TestAudisoft.Application.Grades.UseCases.GradesBusiness
{
    public class GradesBusiness : IGradesBusiness
    {
        private readonly IGradesRepository _gradesRepository;
        private readonly IMapper _mapper;

        public GradesBusiness(IMapper mapper, IGradesRepository gradesRepository)
        {
            _mapper = mapper;
            _gradesRepository = gradesRepository;
        }
    }
}
