using Microsoft.EntityFrameworkCore;
using TestAudisoft.Entities;

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

            modelBuilder.HasDefaultSchema("Data");

            ConfigureProfessor(modelBuilder);
            ConfigureStudent(modelBuilder);
            ConfigureGrades(modelBuilder);
        }

        private static void ConfigureProfessor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfessorEntity>(entity =>
            {
                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(150);
            });
        }

        private static void ConfigureStudent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentEntity>(entity =>
            {
                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(150);
            });
        }

        private static void ConfigureGrades(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GradesEntity>(entity =>
            {
                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.Grade)
                    .IsRequired();

                entity.Property(x => x.StudentId)
                    .IsRequired();

                entity.Property(x => x.ProfessorId)
                    .IsRequired();

                entity.HasOne(x => x.Student)
                    .WithMany(x => x.Grades)
                    .HasForeignKey(x => x.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Professor)
                    .WithMany(x => x.Grades)
                    .HasForeignKey(x => x.ProfessorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
