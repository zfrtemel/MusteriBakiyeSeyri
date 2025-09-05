namespace Application.DTOs;

/// <summary>
/// Müşteri bakiye bilgilerinin API response modeli
/// </summary>
public class CustomerBalanceDto
{
    public int CustomerId { get; set; }
    public string CustomerTitle { get; set; } = string.Empty;
    public decimal MaximumDebt { get; set; }
    public DateTime MaximumDebtDate { get; set; }
    public decimal CurrentBalance { get; set; }
    public List<DailyBalanceDto> DailyBalances { get; set; } = new();
}

public class DailyBalanceDto
{
    public DateTime Date { get; set; }
    public decimal Balance { get; set; }
    public List<TransactionDto> Transactions { get; set; } = new();
}

public class TransactionDto
{
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = string.Empty;
    public int DocumentId { get; set; }
}