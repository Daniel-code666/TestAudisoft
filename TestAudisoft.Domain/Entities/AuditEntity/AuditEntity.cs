using System.ComponentModel.DataAnnotations.Schema;

namespace TestAudisoft.Entities
{
    public class AuditEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime ModificationDate { get; set; }
    }
}
