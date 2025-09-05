using Application.DTOs;

namespace Web.ViewModels;

/// <summary>
/// Müşteri bakiye seyri sayfası için view model
/// </summary>
public class CustomerBalanceViewModel
{
    public int CustomerId { get; set; }
    public string CustomerTitle { get; set; } = string.Empty;
    public List<CustomerDto> Customers { get; set; } = new();
    public CustomerBalanceDto? BalanceData { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public bool HasData => BalanceData != null;

    /// <summary>
    /// Maksimum borç bilgisi özet formatında
    /// </summary>
    public string MaxDebtSummary => BalanceData != null
        ? $"{BalanceData.MaximumDebt:C} ({BalanceData.MaximumDebtDate:dd.MM.yyyy})"
        : "Veri yok";

    /// <summary>
    /// Mevcut bakiye durumu
    /// </summary>
    public string CurrentBalanceStatus => BalanceData != null
        ? $"{BalanceData.CurrentBalance:C}"
        : "Veri yok";
}
