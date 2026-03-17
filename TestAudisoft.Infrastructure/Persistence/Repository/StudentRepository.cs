using Microsoft.EntityFrameworkCore;
using TestAudisoft.Application.Abstractions.Persistence;
using TestAudisoft.Application.Common;
using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Entities;
using TestAudisoft.Enums;

namespace TestAudisoft.Infrastructure.Persistence.Repository
{
    internal class StudentRepository : IStudentRepository
    {
        private readonly TestAudisoftDbContext _context;

        public StudentRepository(TestAudisoftDbContext context)
        {
            _context = context;
        }

        async Task<DbActions> IStudentRepository.CreateStudent(StudentEntity student)
        {
            await _context.Student.AddAsync(student);
            int rows_affected = await _context.SaveChangesAsync();

            return rows_affected > 0 ? DbActions.Created : DbActions.NotCreated;
        }

        async Task<PagedResult<StudentEntity>> IStudentRepository.GetAll(StudentFilter filter)
        {
            IQueryable<StudentEntity> query = _context.Student.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                string first_name = filter.FirstName.Trim().ToLower();
                query = query.Where(x => x.FirstName.ToLower().Contains(first_name));
            }

            if (!string.IsNullOrWhiteSpace(filter.LastName))
            {
                string last_name = filter.LastName.Trim().ToLower();
                query = query.Where(x => x.LastName.ToLower().Contains(last_name));
            }

            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                string email = filter.Email.Trim().ToLower();
                query = query.Where(x => x.Email.ToLower().Contains(email));
            }

            int page_number = filter.PageNumber <= 0 ? 1 : filter.PageNumber;
            int page_size = filter.PageSize <= 0 ? 10 : filter.PageSize;

            int total_records = await query.CountAsync();

            List<StudentEntity> items = await query
                .OrderBy(x => x.Id)
                .Skip((page_number - 1) * page_size)
                .Take(page_size)
                .ToListAsync();

            return new PagedResult<StudentEntity>
            {
                Items = items,
                TotalRecords = total_records,
                PageNumber = page_number,
                PageSize = page_size
            };
        }

        async Task<StudentEntity?> IStudentRepository.GetById(int id)
            => await _context.Student.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);


        async Task<DbActions> IStudentRepository.UpdateStudent(StudentEntity student)
        {
            StudentEntity? student_db = await _context.Student.FirstOrDefaultAsync(x => x.Id == student.Id);

            if (student_db is null)
                return DbActions.NotFound;

            student_db.FirstName = student.FirstName;
            student_db.LastName = student.LastName;
            student_db.Email = student.Email;

            int rows_affected = await _context.SaveChangesAsync();

            return rows_affected > 0 ? DbActions.Updated : DbActions.NotUpdated;
        }
    }
}
