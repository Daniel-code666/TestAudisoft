using Microsoft.EntityFrameworkCore;
using TestAudisoft.Entities;

namespace TestAudisoft.Infrastructure.Persistence
{
    public sealed partial class TestAudisoftDbContext
    {
        public DbSet<ProfessorEntity> Professor { get; set; }
        public DbSet<StudentEntity> Student { get; set; }
        public DbSet<GradesEntity> Grades { get; set; }
    }
}
