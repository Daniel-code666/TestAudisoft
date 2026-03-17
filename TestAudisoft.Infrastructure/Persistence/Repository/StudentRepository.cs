using TestAudisoft.Application.Abstractions.Persistence;
using TestAudisoft.Entities;
using TestAudisoft.Enums;

namespace TestAudisoft.Infrastructure.Persistence.Repository
{
    internal class StudentRepository : IStudentRepository
    {
        private readonly TestAudisoftDbContext _dbContext;

        public StudentRepository(TestAudisoftDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        Task<DbActions> IStudentRepository.CreateStudent(StudentEntity student)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<StudentEntity>> IStudentRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<StudentEntity?> IStudentRepository.GetById(int id)
        {
            throw new NotImplementedException();
        }

        Task<DbActions> IStudentRepository.UpdateStudent(StudentEntity student)
        {
            throw new NotImplementedException();
        }
    }
}
