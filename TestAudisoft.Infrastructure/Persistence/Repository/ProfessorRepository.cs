using Microsoft.EntityFrameworkCore;
using TestAudisoft.Application.Abstractions.Persistence;
using TestAudisoft.Application.Common;
using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Entities;
using TestAudisoft.Enums;

namespace TestAudisoft.Infrastructure.Persistence.Repository
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly TestAudisoftDbContext _context;

        public ProfessorRepository(TestAudisoftDbContext context)
        {
            _context = context;
        }

        async Task<DbActions> IProfessorRepository.CreateProfessor(ProfessorEntity professor)
        {
            await _context.Professor.AddAsync(professor);
            int rows_affected = await _context.SaveChangesAsync();

            return rows_affected > 0 ? DbActions.Created : DbActions.NotCreated;
        }

        async Task<PagedResult<ProfessorEntity>> IProfessorRepository.GetAll(StudentFilter filter)
        {
            IQueryable<ProfessorEntity> query = _context.Professor.AsNoTracking().AsQueryable();

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

            List<ProfessorEntity> items = await query
                .OrderBy(x => x.Id)
                .Skip((page_number - 1) * page_size)
                .Take(page_size)
                .ToListAsync();

            return new PagedResult<ProfessorEntity>
            {
                Items = items,
                TotalRecords = total_records,
                PageNumber = page_number,
                PageSize = page_size
            };
        }

        async Task<ProfessorEntity?> IProfessorRepository.GetById(int id)
            => await _context.Professor.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        async Task<DbActions> IProfessorRepository.UpdateProfessor(ProfessorEntity professor)
        {
            ProfessorEntity? professor_db = await _context.Professor.FirstOrDefaultAsync(x => x.Id == professor.Id);

            if (professor_db is null)
                return DbActions.NotFound;

            professor_db.FirstName = professor.FirstName;
            professor_db.LastName = professor.LastName;
            professor_db.Email = professor.Email;

            int rows_affected = await _context.SaveChangesAsync();

            return rows_affected > 0 ? DbActions.Updated : DbActions.NotUpdated;
        }
    }
}
