namespace Application.DTOs;

public class InvoiceDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal Amount { get; set; }
    public DateTime? PaymentDate { get; set; }
    public bool IsPaid => PaymentDate.HasValue;
}
