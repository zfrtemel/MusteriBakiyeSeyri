using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("musteri_fatura_table")]
public class Invoice : BaseEntity
{
    [Column("MUSTERI_ID")]
    public int CustomerId { get; set; }

    [Column("FATURA_TARIHI")]
    public DateTime InvoiceDate { get; set; }

    [Column("FATURA_TUTARI")]
    public decimal Amount { get; set; }

    [Column("ODEME_TARIHI")]
    public DateTime? PaymentDate { get; set; } = null;


    public virtual Customer Customer { get; set; }
}