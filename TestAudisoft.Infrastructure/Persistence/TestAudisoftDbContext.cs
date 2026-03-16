using Microsoft.EntityFrameworkCore;

namespace TestAudisoft.Infrastructure.Persistence
{
    public sealed partial class TestAudisoftDbContext : DbContext
    {
        public TestAudisoftDbContext(DbContextOptions<TestAudisoftDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
