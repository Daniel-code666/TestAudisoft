using TestAudisoft.Entities;
using TestAudisoft.Enums;

namespace TestAudisoft.Application.Abstractions.Persistence
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentEntity>> GetAll();
        Task<StudentEntity?> GetById(int id);
        Task<DbActions> CreateStudent(StudentEntity student);
        Task<DbActions> UpdateStudent(StudentEntity student);
    }
}
