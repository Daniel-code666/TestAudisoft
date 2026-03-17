using TestAudisoft.Entities;
using TestAudisoft.Enums;

namespace TestAudisoft.Application.Abstractions.Persistence
{
    public interface IProfessorRepository
    {
        Task<IEnumerable<ProfessorEntity>> GetAll();
        Task<ProfessorEntity?> GetById(int id);
        Task<DbActions> CreateProfessor(ProfessorEntity professor);
        Task<DbActions> UpdateProfessor(ProfessorEntity professor);
    }
}
