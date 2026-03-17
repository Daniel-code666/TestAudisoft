using TestAudisoft.Application.Abstractions.Persistence;
using TestAudisoft.Entities;
using TestAudisoft.Enums;

namespace TestAudisoft.Infrastructure.Persistence.Repository
{
    public class GradesRepository : IGradesRepository
    {
        private readonly TestAudisoftDbContext _context;

        public GradesRepository(TestAudisoftDbContext context)
        {
            _context = context;
        }

        Task<DbActions> IGradesRepository.CreateGrades(GradesEntity grades)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<GradesEntity>> IGradesRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<GradesEntity?> IGradesRepository.GetById(int id)
        {
            throw new NotImplementedException();
        }

        Task<DbActions> IGradesRepository.UpdateGrades(GradesEntity grades)
        {
            throw new NotImplementedException();
        }
    }
}
