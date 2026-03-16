using System.ComponentModel.DataAnnotations.Schema;

namespace TestAudisoft.Entities
{
    [Table("Grades")]
    public class GradesEntity : AuditEntity
    {
        #region atributos
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Grade { get; set; }
        #endregion

        #region relaciones
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }
        public StudentEntity Student { get; set; } = null!;

        [ForeignKey("ProfessorId")]
        public int ProfessorId { get; set; }
        public ProfessorEntity Professor { get; set; } = null!;
        #endregion
    }
}
