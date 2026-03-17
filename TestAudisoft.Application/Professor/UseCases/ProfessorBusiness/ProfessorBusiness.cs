using AutoMapper;
using TestAudisoft.Application.Abstractions.Persistence;

namespace TestAudisoft.Application.Professor.UseCases.ProfessorBusiness
{
    public class ProfessorBusiness : IProfessorBusiness
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IMapper _mapper;

        public ProfessorBusiness(IMapper mapper, IProfessorRepository professorRepository)
        {
            _mapper = mapper;
            _professorRepository = professorRepository;
        }
    }
}
