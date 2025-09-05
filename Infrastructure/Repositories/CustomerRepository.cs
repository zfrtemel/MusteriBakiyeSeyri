using Application.Repositories;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext dbContext;

    public CustomerRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    /// <summary>
    /// tüm müşterileri getirir
    /// </summary>
    /// <returns></returns>
    public async Task<IReadOnlyList<Customer>> GetAllAsync()
    {
        return await dbContext.Customers
            .OrderBy(customer => customer.Title)
            .AsNoTracking()
            .ToListAsync();
    }
    /// <summary>
    /// müşteriye ait tüm kayıtları getirir
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    public async Task<Customer?> GetCustomerWithInvoicesAsync(int customerId)
    {
        if (customerId <= 0)
            return null;

        return await dbContext.Customers
            .Include(customer => customer.Invoices)
            .AsNoTracking()
            .FirstOrDefaultAsync(customer => customer.Id == customerId);
    }


    public async Task<CustomerBalanceModel> GetCustomerBalanceAsync(
        int customerId, DateTime? startDate = null, DateTime? endDate = null)
    {
        // 1. Giriş parametrelerini kontrol et
        if (customerId <= 0)
            throw new ArgumentException($"Müşteri bulunamadı: {customerId}");

        // 2. Müşteri bilgilerini veritabanından al
        var customer = await dbContext.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == customerId);

        if (customer == null)
            throw new ArgumentException($"Müşteri bulunamadı: {customerId}");

        // 3. SQL sorgusu ile tüm işlemleri ve kümülatif bakiyeyi hesapla
        // Bu sorgu hem faturaları hem ödemeleri tek bir tabloda birleştirir
        // Window function ile her işlem sonrası bakiyeyi hesaplar
        var sql = @"
WITH transactions AS (
    -- Faturalar: Bakiyeyi artıran işlemler (+)
    SELECT 
        ""FATURA_TARIHI"" AS tx_date,
        ""ID"" AS document_id,
        ""FATURA_TUTARI"" AS amount,
        'Invoice' AS tx_type
    FROM musteri_fatura_table
    WHERE ""MUSTERI_ID"" = @customerId

    UNION ALL
    
    -- Ödemeler: Bakiyeyi azaltan işlemler (–)
    SELECT 
        ""ODEME_TARIHI"" AS tx_date,
        ""ID"" AS document_id,
        -""FATURA_TUTARI"" AS amount,  -- Negatif değer ile bakiyeyi azalt
        'Payment' AS tx_type
    FROM musteri_fatura_table
    WHERE ""MUSTERI_ID"" = @customerId 
      AND ""ODEME_TARIHI"" IS NOT NULL  -- Sadece ödenmiş faturalar
)
SELECT
    tx_date,
    document_id,
    tx_type,
    amount,
    -- Window function ile kümülatif bakiye hesaplama
    SUM(amount) OVER (ORDER BY tx_date ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS balance
FROM transactions
WHERE (@startDate IS NULL OR tx_date >= @startDate)  -- Başlangıç tarihi filtresi
  AND (@endDate IS NULL OR tx_date <= @endDate)      -- Bitiş tarihi filtresi
ORDER BY tx_date;";

        // 4. SQL sorgusunu çalıştır ve sonuçları al
        var transactions = await dbContext.Database
            .SqlQueryRaw<SqlTransactionRow>(
                sql,
                new Npgsql.NpgsqlParameter("customerId", customerId),
                new Npgsql.NpgsqlParameter("startDate", (object?)startDate ?? DBNull.Value) { NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Timestamp },
                new Npgsql.NpgsqlParameter("endDate", (object?)endDate ?? DBNull.Value) { NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Timestamp }
            )
            .ToListAsync();

        // 5. Eğer hiç işlem yoksa boş model döndür
        if (!transactions.Any())
            return CustomerBalanceModel.Create(customerId, customer.Title, [], 0, DateTime.Today, 0);

        // 6. İşlemleri günlere göre grupla ve DailyBalanceModel oluştur
        var dailyBalances = transactions
            .GroupBy(t => t.tx_date.Date)  // Aynı günkü işlemleri grupla
            .Select(g =>
            {
                // Her günkü işlemleri TransactionModel'e çevir
                var txModels = g.Select(t =>
                    TransactionModel.Create(
                        t.tx_date,
                        t.amount,
                        t.tx_type == "Invoice" ? TransactionType.Invoice : TransactionType.Payment,
                        t.document_id))
                    .OrderBy(t => t.Type == TransactionType.Invoice ? 0 : 1);  // Önce faturalar, sonra ödemeler

                // Gün sonu bakiyesini al (o günkü en yüksek bakiye)
                var dayBalance = g.Max(x => x.balance);

                return DailyBalanceModel.Create(g.Key, dayBalance, txModels);
            })
            .ToList();

        // 7. Maksimum borç miktarı ve tarihini hesapla
        var maximumDebt = dailyBalances.Max(x => x.Balance);
        var maximumDebtDate = dailyBalances.First(x => x.Balance == maximumDebt).TransactionDate;
        
        // 8. Mevcut bakiyeyi al (son günün bakiyesi)
        var currentBalance = dailyBalances.Last().Balance;

        // 9. Sonuç modelini oluştur ve döndür
        return CustomerBalanceModel.Create(
            customer.Id,
            customer.Title,
            dailyBalances,
            maximumDebt,
            maximumDebtDate,
            currentBalance
        );
    }


    /// <summary>
    /// Müşterinin maksimum borca ulaştığı tarihi döndürür
    /// </summary>
    /// <param name="customerId">Müşteri kimlik numarası</param>
    /// <returns>Maksimum borç tarihi</returns>
    public async Task<DateTime> GetCustomerMaxDebtDateAsync(int customerId)
    {
        // Ana bakiye hesaplama metodunu kullan ve maksimum borç tarihini al
        var customerBalance = await GetCustomerBalanceAsync(customerId);
        return customerBalance.MaximumDebtDate;
    }

    /// <summary>
    /// Müşterinin belirtilen tarih aralığındaki günlük bakiye hareketlerini listeler
    /// </summary>
    /// <param name="customerId">Müşteri kimlik numarası</param>
    /// <param name="startDate">Başlangıç tarihi (opsiyonel)</param>
    /// <param name="endDate">Bitiş tarihi (opsiyonel)</param>
    /// <returns>Günlük bakiye listesi</returns>
    public async Task<List<DailyBalanceModel>> GetCustomerDailyBalancesAsync(int customerId, DateTime? startDate = null, DateTime? endDate = null)
    {
        // Ana bakiye hesaplama metodunu kullan ve günlük bakiyeleri al
        var customerBalance = await GetCustomerBalanceAsync(customerId, startDate, endDate);
        return customerBalance.DailyBalances.ToList();
    }

    /// <summary>
    /// Müşterinin mevcut toplam bakiyesini hesaplar ve döndürür
    /// Bu metot performans için doğrudan SQL ile hesaplama yapar
    /// </summary>
    /// <param name="customerId">Müşteri kimlik numarası</param>
    /// <returns>Mevcut bakiye tutarı</returns>
    public async Task<decimal> GetCustomerCurrentBalanceAsync(int customerId)
    {
        // Giriş parametresini kontrol et
        if (customerId <= 0)
            return 0;

        // Tek SQL sorgusunda toplam fatura ve ödeme tutarlarını hesapla
        var balanceData = await dbContext.Customers
            .Where(customer => customer.Id == customerId)
            .Select(customer => new
            {
                // Tüm faturaların toplam tutarı
                TotalInvoices = customer.Invoices.Sum(invoice => invoice.Amount),
                // Sadece ödenmiş faturaların toplam tutarı
                TotalPayments = customer.Invoices
                    .Where(invoice => invoice.PaymentDate.HasValue)
                    .Sum(invoice => invoice.Amount)
            })
            .FirstOrDefaultAsync();

        // Müşteri bulunamadıysa 0 döndür
        if (balanceData == null)
            return 0;

        // Mevcut bakiye = Toplam Faturalar - Toplam Ödemeler
        return balanceData.TotalInvoices - balanceData.TotalPayments;
    }
}