using Domain.Models;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Models;

public class CustomerBalanceModelTests
{
    [Fact]
    public void Create_WithValidParameters_ShouldCreateModel()
    {
        // Arrange
        var customerId = 1;
        var customerTitle = "Test Customer";
        var maximumDebt = 5000m;
        var maximumDebtDate = new DateTime(2023, 6, 15);
        var currentBalance = 2500m;
        var dailyBalances = new List<DailyBalanceModel>
        {
            DailyBalanceModel.Create(
                new DateTime(2023, 1, 1),
                1000m,
                new List<TransactionModel>()
            )
        };

        // Act
        var result = CustomerBalanceModel.Create(
            customerId,
            customerTitle,
            dailyBalances,
            maximumDebt,
            maximumDebtDate,
            currentBalance
        );

        // Assert
        result.Should().NotBeNull();
        result.CustomerId.Should().Be(customerId);
        result.CustomerTitle.Should().Be(customerTitle);
        result.MaximumDebt.Should().Be(maximumDebt);
        result.MaximumDebtDate.Should().Be(maximumDebtDate);
        result.CurrentBalance.Should().Be(currentBalance);
        result.DailyBalances.Should().HaveCount(1);
        result.DailyBalances.Should().BeEquivalentTo(dailyBalances);
    }

    [Fact]
    public void Create_WithEmptyDailyBalances_ShouldCreateModelWithEmptyCollection()
    {
        // Arrange
        var customerId = 1;
        var customerTitle = "Test Customer";
        var maximumDebt = 0m;
        var maximumDebtDate = DateTime.Today;
        var currentBalance = 0m;
        var dailyBalances = new List<DailyBalanceModel>();

        // Act
        var result = CustomerBalanceModel.Create(
            customerId,
            customerTitle,
            dailyBalances,
            maximumDebt,
            maximumDebtDate,
            currentBalance
        );

        // Assert
        result.Should().NotBeNull();
        result.DailyBalances.Should().BeEmpty();
    }
}

public class DailyBalanceModelTests
{
    [Fact]
    public void Create_WithValidParameters_ShouldCreateModel()
    {
        // Arrange
        var transactionDate = new DateTime(2023, 1, 15);
        var balance = 1500m;
        var transactions = new List<TransactionModel>
        {
            TransactionModel.Create(
                transactionDate,
                1000m,
                TransactionType.Invoice,
                1
            ),
            TransactionModel.Create(
                transactionDate,
                -500m,
                TransactionType.Payment,
                1
            )
        };

        // Act
        var result = DailyBalanceModel.Create(transactionDate, balance, transactions);

        // Assert
        result.Should().NotBeNull();
        result.TransactionDate.Should().Be(transactionDate);
        result.Balance.Should().Be(balance);
        result.Transactions.Should().HaveCount(2);
        result.Transactions.Should().BeEquivalentTo(transactions);
    }

    [Fact]
    public void Create_WithEmptyTransactions_ShouldCreateModelWithEmptyCollection()
    {
        // Arrange
        var transactionDate = new DateTime(2023, 1, 15);
        var balance = 0m;
        var transactions = new List<TransactionModel>();

        // Act
        var result = DailyBalanceModel.Create(transactionDate, balance, transactions);

        // Assert
        result.Should().NotBeNull();
        result.Transactions.Should().BeEmpty();
    }
}

public class TransactionModelTests
{
    [Fact]
    public void Create_WithInvoiceTransaction_ShouldCreateModel()
    {
        // Arrange
        var transactionDate = new DateTime(2023, 1, 15);
        var amount = 1000m;
        var type = TransactionType.Invoice;
        var documentId = 123;

        // Act
        var result = TransactionModel.Create(transactionDate, amount, type, documentId);

        // Assert
        result.Should().NotBeNull();
        result.TransactionDate.Should().Be(transactionDate);
        result.Amount.Should().Be(amount);
        result.Type.Should().Be(type);
        result.DocumentId.Should().Be(documentId);
    }

    [Fact]
    public void Create_WithPaymentTransaction_ShouldCreateModel()
    {
        // Arrange
        var transactionDate = new DateTime(2023, 1, 20);
        var amount = -1000m;
        var type = TransactionType.Payment;
        var documentId = 123;

        // Act
        var result = TransactionModel.Create(transactionDate, amount, type, documentId);

        // Assert
        result.Should().NotBeNull();
        result.TransactionDate.Should().Be(transactionDate);
        result.Amount.Should().Be(amount);
        result.Type.Should().Be(type);
        result.DocumentId.Should().Be(documentId);
    }

    [Theory]
    [InlineData(TransactionType.Invoice)]
    [InlineData(TransactionType.Payment)]
    public void Create_WithDifferentTransactionTypes_ShouldCreateModelWithCorrectType(TransactionType type)
    {
        // Arrange
        var transactionDate = DateTime.Today;
        var amount = 500m;
        var documentId = 1;

        // Act
        var result = TransactionModel.Create(transactionDate, amount, type, documentId);

        // Assert
        result.Type.Should().Be(type);
    }
}
