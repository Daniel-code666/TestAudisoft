using TestAudisoft.Application.Common;
using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Entities;
using TestAudisoft.Enums;

namespace TestAudisoft.Application.Abstractions.Persistence
{
    public interface IStudentRepository
    {
        Task<DbActions> CreateStudent(StudentEntity student);
        Task<PagedResult<StudentEntity>> GetAll(StudentFilter filter);
        Task<StudentEntity?> GetById(int id);
        Task<DbActions> UpdateStudent(StudentEntity student);
    }
}
