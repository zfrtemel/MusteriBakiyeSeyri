namespace Domain.Models;

/// <summary>
/// Müşterinin borç ve bakiye bilgilerini tutan model
/// </summary>
public class CustomerBalanceModel
{
    /// <summary>
    /// Müşteri ID
    /// </summary>
    public int CustomerId { get; private set; }

    /// <summary>
    /// Müşteri Ünvanı
    /// </summary>
    public string CustomerTitle { get; private set; }

    /// <summary>
    /// Günlük bakiye hareketleri
    /// </summary>
    public IReadOnlyList<DailyBalanceModel> DailyBalances { get; private set; }

    /// <summary>
    /// Maksimum borç tutarı
    /// </summary>
    public decimal MaximumDebt { get; private set; }

    /// <summary>
    /// Maksimum borca ulaşılan tarih
    /// </summary>
    public DateTime MaximumDebtDate { get; private set; }

    /// <summary>
    /// Mevcut bakiye
    /// </summary>
    public decimal CurrentBalance { get; private set; }

    private CustomerBalanceModel() { }

    public static CustomerBalanceModel Create(
        int customerId,
        string customerTitle,
        IEnumerable<DailyBalanceModel> dailyBalances,
        decimal maximumDebt,
        DateTime maximumDebtDate,
        decimal currentBalance)
    {
        return new CustomerBalanceModel
        {
            CustomerId = customerId,
            CustomerTitle = customerTitle,
            DailyBalances = dailyBalances.ToList().AsReadOnly(),
            MaximumDebt = maximumDebt,
            MaximumDebtDate = maximumDebtDate,
            CurrentBalance = currentBalance
        };
    }
}

/// <summary>
/// Günlük bakiye bilgilerini tutan model
/// </summary>
public class DailyBalanceModel
{
    /// <summary>
    /// İşlem tarihi
    /// </summary>
    public DateTime TransactionDate { get; private set; }

    /// <summary>
    /// Gün sonu bakiyesi
    /// </summary>
    public decimal Balance { get; private set; }

    /// <summary>
    /// Gün içindeki işlemler
    /// </summary>
    public IReadOnlyList<TransactionModel> Transactions { get; private set; }

    private DailyBalanceModel() { }

    public static DailyBalanceModel Create(
        DateTime transactionDate,
        decimal balance,
        IEnumerable<TransactionModel> transactions)
    {
        return new DailyBalanceModel
        {
            TransactionDate = transactionDate,
            Balance = balance,
            Transactions = transactions.ToList().AsReadOnly()
        };
    }
}

/// <summary>
/// Fatura veya ödeme işlemlerini tutan model
/// </summary>
public class TransactionModel
{
    /// <summary>
    /// İşlem tarihi
    /// </summary>
    public DateTime TransactionDate { get; private set; }

    /// <summary>
    /// İşlem tutarı (fatura için pozitif, ödeme için negatif)
    /// </summary>
    public decimal Amount { get; private set; }

    /// <summary>
    /// İşlem tipi (INVOICE veya PAYMENT)
    /// </summary>
    public TransactionType Type { get; private set; }

    /// <summary>
    /// İşlem belge numarası (fatura veya ödeme ID'si)
    /// </summary>
    public int DocumentId { get; private set; }

    private TransactionModel() { }

    public static TransactionModel Create(
        DateTime transactionDate,
        decimal amount,
        TransactionType type,
        int documentId)
    {
        return new TransactionModel
        {
            TransactionDate = transactionDate,
            Amount = amount,
            Type = type,
            DocumentId = documentId
        };
    }
}

/// <summary>
/// İşlem tiplerini belirten enum
/// </summary>
public enum TransactionType
{
    /// <summary>
    /// Fatura işlemi
    /// </summary>
    Invoice,

    /// <summary>
    /// Ödeme işlemi
    /// </summary>
    Payment
}

public record SqlTransactionRow(
    DateTime tx_date,
    int document_id,
    string tx_type,
    decimal amount,
    decimal balance);