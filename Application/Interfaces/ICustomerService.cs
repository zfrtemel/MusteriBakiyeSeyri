using Application.DTOs;

namespace Application.Interfaces;

public interface ICustomerService
{
    /// <summary>
    /// Tüm müşterileri getirir
    /// </summary>
    Task<List<CustomerDto>> GetAllCustomersAsync();

    /// <summary>
    /// ID'ye göre müşteri getirir
    /// </summary>
    Task<CustomerDto> GetCustomerByIdAsync(int id);

    /// <summary>
    /// Müşterinin bakiye bilgilerini getirir
    /// </summary>
    Task<CustomerBalanceDto> GetCustomerBalanceAsync(int customerId, DateTime? startDate = null, DateTime? endDate = null);

    /// <summary>
    /// Müşterinin mevcut bakiye durumunu getirir
    /// </summary>
    Task<decimal> GetCustomerCurrentBalanceAsync(int customerId);
}
