namespace Application.DTOs;

public class CustomerDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal CurrentBalance { get; set; }
    public decimal MaximumDebt { get; set; }
    public DateTime? MaximumDebtDate { get; set; }
}
