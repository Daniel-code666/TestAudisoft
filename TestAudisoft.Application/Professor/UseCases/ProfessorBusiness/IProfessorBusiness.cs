using TestAudisoft.Application.Common;
using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Application.Professor.Dtos;
using TestAudisoft.Enums;

namespace TestAudisoft.Application.Professor.UseCases.ProfessorBusiness
{
    public interface IProfessorBusiness
    {
        Task<PagedResult<ProfessorDto>> GetAll(StudentFilter filter);
        Task<ProfessorDto?> GetById(int id);
        Task<DbActions> CreateProfessor(ProfessorCreateDto professor);
        Task<DbActions> UpdateProfessor(ProfessorUpdateDto professor);
        Task<ProfessorWithGradesDto?> GetProfessorWithGrades(int professor_id);
    }
}
