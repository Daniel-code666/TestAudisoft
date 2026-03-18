using TestAudisoft.Application.Common;
using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Application.Grades.Dtos;
using TestAudisoft.Enums;

namespace TestAudisoft.Application.Grades.UseCases.GradesBusiness
{
    public interface IGradesBusiness
    {
        Task<PagedResult<GradeDto>> GetAll(GradesFilter filter);
        Task<GradeDto?> GetById(int id);
        Task<DbActions> CreateGrades(GradeCreateDto grades);
        Task<DbActions> UpdateGrades(GradeUpdateDto grades);
        Task<IEnumerable<GradeDto>> GetByStudentId(int student_id);
        Task<IEnumerable<GradeDto>> GetByProfessorId(int professor_id);
        Task<DbActions> DeleteGrades(int id);
    }
}
