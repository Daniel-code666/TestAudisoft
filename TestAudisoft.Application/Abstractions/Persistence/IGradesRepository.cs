using TestAudisoft.Entities;
using TestAudisoft.Enums;

namespace TestAudisoft.Application.Abstractions.Persistence
{
    public interface IGradesRepository
    {
        Task<IEnumerable<GradesEntity>> GetAll();
        Task<GradesEntity?> GetById(int id);
        Task<DbActions> CreateGrades(GradesEntity grades);
        Task<DbActions> UpdateGrades(GradesEntity grades);
    }
}
