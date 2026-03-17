using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAudisoft.Entities
{
    [Table("Grades")]
    public class GradesEntity : AuditEntity
    {
        #region atributos
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Grade { get; set; }
        #endregion

        #region relaciones
        [ForeignKey(nameof(StudentId))]
        public int StudentId { get; set; }
        public StudentEntity Student { get; set; } = null!;

        [ForeignKey(nameof(ProfessorId))]
        public int ProfessorId { get; set; }
        public ProfessorEntity Professor { get; set; } = null!;
        #endregion
    }
}
