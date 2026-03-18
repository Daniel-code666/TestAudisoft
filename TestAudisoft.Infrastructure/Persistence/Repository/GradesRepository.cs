using Microsoft.EntityFrameworkCore;
using TestAudisoft.Application.Abstractions.Persistence;
using TestAudisoft.Application.Common;
using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Entities;
using TestAudisoft.Enums;

namespace TestAudisoft.Infrastructure.Persistence.Repository
{
    public class GradesRepository : IGradesRepository
    {
        private readonly TestAudisoftDbContext _context;

        public GradesRepository(TestAudisoftDbContext context)
        {
            _context = context;
        }

        async Task<DbActions> IGradesRepository.CreateGrades(GradesEntity grades)
        {
            bool student_exists = await _context.Student.AnyAsync(x => x.Id == grades.StudentId);

            if (!student_exists)
                return DbActions.NotFound;

            bool professor_exists = await _context.Professor.AnyAsync(x => x.Id == grades.ProfessorId);

            if (!professor_exists)
                return DbActions.NotFound;

            await _context.Grades.AddAsync(grades);
            int rows_affected = await _context.SaveChangesAsync();

            return rows_affected > 0 ? DbActions.Created : DbActions.NotCreated;
        }

        async Task<PagedResult<GradesEntity>> IGradesRepository.GetAll(GradesFilter filter)
        {
            IQueryable<GradesEntity> query = _context.Grades.AsNoTracking().Include(x => x.Student).Include(x => x.Professor).AsQueryable();

            if (filter.Grade.HasValue)
                query = query.Where(x => x.Grade == filter.Grade.Value);

            if (filter.CreationDateFrom.HasValue)
                query = query.Where(x => x.CreationDate >= filter.CreationDateFrom.Value);

            if (filter.CreationDateTo.HasValue)
                query = query.Where(x => x.CreationDate <= filter.CreationDateTo.Value);

            if (filter.ModificationDateFrom.HasValue)
                query = query.Where(x => x.ModificationDate >= filter.ModificationDateFrom.Value);

            if (filter.ModificationDateTo.HasValue)
                query = query.Where(x => x.ModificationDate <= filter.ModificationDateTo.Value);

            int page_number = filter.PageNumber <= 0 ? 1 : filter.PageNumber;
            int page_size = filter.PageSize <= 0 ? 10 : filter.PageSize;

            int total_records = await query.CountAsync();

            List<GradesEntity> items = await query
                .OrderBy(x => x.Id)
                .Skip((page_number - 1) * page_size)
                .Take(page_size)
                .ToListAsync();

            return new PagedResult<GradesEntity>
            {
                Items = items,
                TotalRecords = total_records,
                PageNumber = page_number,
                PageSize = page_size
            };
        }

        async Task<GradesEntity?> IGradesRepository.GetById(int id)
            => await _context.Grades.AsNoTracking().Include(x => x.Professor).Include(x => x.Student).FirstOrDefaultAsync(x => x.Id == id);

        async Task<GradesEntity?> IGradesRepository.GetByIdWithRelations(int id)
            => await _context.Grades.AsNoTracking().Include(x => x.Student).Include(x => x.Professor).FirstOrDefaultAsync(x => x.Id == id);

        async Task<IEnumerable<GradesEntity>> IGradesRepository.GetByStudentId(int student_id)
            => await _context.Grades.AsNoTracking().Include(x => x.Student).Include(x => x.Professor).Where(x => x.StudentId == student_id).OrderBy(x => x.Id).ToListAsync();

        async Task<IEnumerable<GradesEntity>> IGradesRepository.GetByProfessorId(int professor_id)
            => await _context.Grades.AsNoTracking().Include(x => x.Student).Include(x => x.Professor).Where(x => x.ProfessorId == professor_id).OrderBy(x => x.Id).ToListAsync();

        async Task<DbActions> IGradesRepository.UpdateGrades(GradesEntity grades)
        {
            GradesEntity? grades_db = await _context.Grades.FirstOrDefaultAsync(x => x.Id == grades.Id);

            if (grades_db is null)
                return DbActions.NotFound;

            bool student_exists = await _context.Student.AnyAsync(x => x.Id == grades.StudentId);

            if (!student_exists)
                return DbActions.NotFound;

            bool professor_exists = await _context.Professor.AnyAsync(x => x.Id == grades.ProfessorId);

            if (!professor_exists)
                return DbActions.NotFound;

            grades_db.Name = grades.Name;
            grades_db.Grade = grades.Grade;
            grades_db.StudentId = grades.StudentId;
            grades_db.ProfessorId = grades.ProfessorId;

            int rows_affected = await _context.SaveChangesAsync();

            return rows_affected > 0 ? DbActions.Updated : DbActions.NotUpdated;
        }

        async Task<DbActions> IGradesRepository.DeleteGrades(int id)
        {
            GradesEntity? grades_db = await _context.Grades.FirstOrDefaultAsync(x => x.Id == id);
            if (grades_db is null)
                return DbActions.NotFound;
            _context.Grades.Remove(grades_db);
            int rows_affected = await _context.SaveChangesAsync();
            return rows_affected > 0 ? DbActions.Deleted : DbActions.NotDeleted;
        }
    }
}
