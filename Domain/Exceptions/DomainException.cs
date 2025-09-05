namespace Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
    protected DomainException(string message, Exception innerException) : base(message, innerException) { }
}

public class BusinessException : DomainException
{
    public BusinessException(string message) : base(message) { }
    public BusinessException(string message, Exception innerException) : base(message, innerException) { }
}

public class CustomerNotFoundException : DomainException
{
    public CustomerNotFoundException(int customerId) 
        : base($"Müşteri bulunamadı. ID: {customerId}") { }
}

public class InvalidDateRangeException : DomainException
{
    public InvalidDateRangeException() 
        : base("Geçersiz tarih aralığı.") { }
}

public class NoInvoicesFoundException : DomainException
{
    public NoInvoicesFoundException(int customerId) 
        : base($"Müşteriye ait fatura bulunamadı. ID: {customerId}") { }
}