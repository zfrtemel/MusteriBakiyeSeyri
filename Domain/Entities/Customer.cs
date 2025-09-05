using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("musteri_tanim_table")]
public class Customer : BaseEntity
{
    [Column("UNVAN")]
    [StringLength(255)]
    public string Title { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; }
}