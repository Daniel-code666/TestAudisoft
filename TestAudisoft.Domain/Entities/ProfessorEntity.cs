using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAudisoft.Entities
{
    [Table("Professor")]
    public class ProfessorEntity : AuditEntity
    {
        #region Atributos
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        #endregion

        #region Relaciones
        public ICollection<GradesEntity> Grades { get; set; } = [];
        #endregion
    }
}
