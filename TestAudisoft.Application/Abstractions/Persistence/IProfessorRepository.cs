using TestAudisoft.Application.Common;
using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Entities;
using TestAudisoft.Enums;

namespace TestAudisoft.Application.Abstractions.Persistence
{
    public interface IProfessorRepository
    {
        Task<DbActions> CreateProfessor(ProfessorEntity professor);
        Task<PagedResult<ProfessorEntity>> GetAll(StudentFilter filter);
        Task<ProfessorEntity?> GetById(int id);
        Task<DbActions> UpdateProfessor(ProfessorEntity professor);
    }
}
