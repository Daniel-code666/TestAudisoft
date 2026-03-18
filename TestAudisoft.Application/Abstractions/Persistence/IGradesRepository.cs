using TestAudisoft.Application.Common;
using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Entities;
using TestAudisoft.Enums;

namespace TestAudisoft.Application.Abstractions.Persistence
{
    public interface IGradesRepository
    {
        Task<PagedResult<GradesEntity>> GetAll(GradesFilter filter);
        Task<GradesEntity?> GetById(int id);
        Task<DbActions> CreateGrades(GradesEntity grades);
        Task<DbActions> UpdateGrades(GradesEntity grades);
        Task<IEnumerable<GradesEntity>> GetByStudentId(int student_id);
        Task<IEnumerable<GradesEntity>> GetByProfessorId(int professor_id);
        Task<GradesEntity?> GetByIdWithRelations(int id);
        Task<DbActions> DeleteGrades(int id);
    }
}
