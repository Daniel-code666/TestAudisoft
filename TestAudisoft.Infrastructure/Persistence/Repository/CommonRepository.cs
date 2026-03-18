using Microsoft.EntityFrameworkCore;
using TestAudisoft.Application.Abstractions.Persistence;

namespace TestAudisoft.Infrastructure.Persistence.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private readonly TestAudisoftDbContext _context;

        public CommonRepository(TestAudisoftDbContext context)
        {
            _context = context;
        }

        async Task<bool> ICommonRepository.CheckIfEmailExists(string email)
            => await _context.Student.AnyAsync(x => x.Email.ToUpper() == email.ToUpper()) || await _context.Professor.AnyAsync(x => x.Email.ToUpper() == email.ToUpper());

        async Task<bool> ICommonRepository.CheckIfEmailExists(string email, int exclude_id, bool is_student)
        {
            string normalized_email = email.Trim().ToUpper();

            bool email_exists_in_students = await _context.Student.AnyAsync(x => x.Email.ToUpper() == normalized_email && (!is_student || x.Id != exclude_id));

            bool email_exists_in_professors = await _context.Professor.AnyAsync(x => x.Email.ToUpper() == normalized_email && (is_student || x.Id != exclude_id));

            return email_exists_in_students || email_exists_in_professors;
        }

        async Task<bool> ICommonRepository.CheckIfGradeNameExists(string name)
            => await _context.Grades.AnyAsync(x => x.Name.ToUpper() == name.Trim().ToUpper());

        async Task<bool> ICommonRepository.CheckIfGradeNameExists(string name, int exclude_id)
        {
            string normalized_name = name.Trim().ToUpper();

            return await _context.Grades.AnyAsync(x => x.Name.ToUpper() == normalized_name && x.Id != exclude_id);
        }
    }
}
