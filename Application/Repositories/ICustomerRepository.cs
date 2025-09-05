using Domain.Entities;
using Domain.Models;

namespace Application.Repositories;

public interface ICustomerRepository
{
    /// <summary>
    /// Tüm müşterileri getirir
    /// </summary>
    Task<IReadOnlyList<Customer>> GetAllAsync();

    /// <summary>
    /// Müşteriyi fatura bilgileri ile birlikte getirir
    /// </summary>
    Task<Customer?> GetCustomerWithInvoicesAsync(int customerId);

    /// <summary>
    /// Müşterinin belirli tarih aralığındaki bakiye bilgilerini hesaplar
    /// </summary>
    Task<CustomerBalanceModel> GetCustomerBalanceAsync(int customerId, DateTime? startDate = null, DateTime? endDate = null);

    /// <summary>
    /// Müşterinin maksimum borç tutarına ulaştığı tarihi bulur
    /// </summary>
    Task<DateTime> GetCustomerMaxDebtDateAsync(int customerId);

    /// <summary>
    /// Müşterinin günlük bakiye seyri bilgilerini getirir
    /// </summary>
    Task<List<DailyBalanceModel>> GetCustomerDailyBalancesAsync(int customerId, DateTime? startDate = null, DateTime? endDate = null);

    /// <summary>
    /// Müşterinin mevcut bakiye durumunu getirir
    /// </summary>
    Task<decimal> GetCustomerCurrentBalanceAsync(int customerId);
}