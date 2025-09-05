using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
    }
}
