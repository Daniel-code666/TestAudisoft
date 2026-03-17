using TestAudisoft.Application.Abstractions.Persistence;
using TestAudisoft.Entities;
using TestAudisoft.Enums;

namespace TestAudisoft.Infrastructure.Persistence.Repository
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly TestAudisoftDbContext _context;

        public ProfessorRepository(TestAudisoftDbContext context)
        {
            _context = context;
        }

        Task<DbActions> IProfessorRepository.CreateProfessor(ProfessorEntity professor)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ProfessorEntity>> IProfessorRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<ProfessorEntity?> IProfessorRepository.GetById(int id)
        {
            throw new NotImplementedException();
        }

        Task<DbActions> IProfessorRepository.UpdateProfessor(ProfessorEntity professor)
        {
            throw new NotImplementedException();
        }
    }
}
