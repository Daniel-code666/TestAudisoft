using TestAudisoft.Application.Common;
using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Application.Student.Dtos;
using TestAudisoft.Enums;

namespace TestAudisoft.Application.Student.UseCases.StudentBusiness
{
    public interface IStudentBusiness
    {
        Task<PagedResult<StudentDto>> GetAll(StudentFilter filter);
        Task<StudentDto?> GetById(int id);
        Task<DbActions> CreateStudent(StudentCreateDto student);
        Task<DbActions> UpdateStudent(StudentUpdateDto student);
        Task<StudentWithGradesDto?> GetStudentWithGrades(int student_id);
        Task<DbActions> DeleteStudent(int id);
    }
}
