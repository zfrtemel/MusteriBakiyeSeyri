using Application.DTOs;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.UnitTests.Services;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CustomerService _sut;

    public CustomerServiceTests()
    {
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _mapperMock = new Mock<IMapper>();
        _sut = new CustomerService(_customerRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllCustomersAsync_ShouldReturnMappedCustomers()
    {
        // Arrange
        var customers = new List<Customer>
        {
            new() { Id = 1, Title = "Customer 1" },
            new() { Id = 2, Title = "Customer 2" }
        };

        var customerDtos = new List<CustomerDto>
        {
            new() { Id = 1, Title = "Customer 1" },
            new() { Id = 2, Title = "Customer 2" }
        };

        _customerRepositoryMock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(customers);

        _mapperMock.Setup(x => x.Map<List<CustomerDto>>(customers))
            .Returns(customerDtos);

        // Act
        var result = await _sut.GetAllCustomersAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(customerDtos);
        
        _customerRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        _mapperMock.Verify(x => x.Map<List<CustomerDto>>(customers), Times.Once);
    }

    [Fact]
    public async Task GetCustomerByIdAsync_WithValidId_ShouldReturnMappedCustomer()
    {
        // Arrange
        var customerId = 1;
        var customer = new Customer { Id = customerId, Title = "Test Customer" };
        var customerDto = new CustomerDto { Id = customerId, Title = "Test Customer" };

        _customerRepositoryMock.Setup(x => x.GetCustomerWithInvoicesAsync(customerId))
            .ReturnsAsync(customer);

        _mapperMock.Setup(x => x.Map<CustomerDto>(customer))
            .Returns(customerDto);

        // Act
        var result = await _sut.GetCustomerByIdAsync(customerId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(customerDto);
        
        _customerRepositoryMock.Verify(x => x.GetCustomerWithInvoicesAsync(customerId), Times.Once);
        _mapperMock.Verify(x => x.Map<CustomerDto>(customer), Times.Once);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public async Task GetCustomerByIdAsync_WithInvalidId_ShouldThrowArgumentException(int invalidId)
    {
        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => 
            _sut.GetCustomerByIdAsync(invalidId));
        
        exception.Message.Should().Be("Geçersiz müşteri ID'si.");
        
        _customerRepositoryMock.Verify(x => x.GetCustomerWithInvoicesAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task GetCustomerByIdAsync_WithNonExistentId_ShouldThrowBusinessException()
    {
        // Arrange
        var customerId = 999;

        _customerRepositoryMock.Setup(x => x.GetCustomerWithInvoicesAsync(customerId))
            .ReturnsAsync((Customer?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BusinessException>(() => 
            _sut.GetCustomerByIdAsync(customerId));
        
        exception.Message.Should().Be($"ID: {customerId} olan müşteri bulunamadı.");
        
        _customerRepositoryMock.Verify(x => x.GetCustomerWithInvoicesAsync(customerId), Times.Once);
        _mapperMock.Verify(x => x.Map<CustomerDto>(It.IsAny<Customer>()), Times.Never);
    }

    [Fact]
    public async Task GetCustomerBalanceAsync_WithValidParameters_ShouldReturnMappedBalance()
    {
        // Arrange
        var customerId = 1;
        var startDate = new DateTime(2023, 1, 1);
        var endDate = new DateTime(2023, 12, 31);

        var balanceModel = CustomerBalanceModel.Create(
            customerId,
            "Test Customer",
            new List<DailyBalanceModel>(),
            1000m,
            new DateTime(2023, 6, 15),
            500m
        );

        var balanceDto = new CustomerBalanceDto
        {
            CustomerId = customerId,
            CustomerTitle = "Test Customer",
            MaximumDebt = 1000m,
            MaximumDebtDate = new DateTime(2023, 6, 15),
            CurrentBalance = 500m
        };

        _customerRepositoryMock.Setup(x => x.GetCustomerBalanceAsync(customerId, startDate, endDate))
            .ReturnsAsync(balanceModel);

        _mapperMock.Setup(x => x.Map<CustomerBalanceDto>(balanceModel))
            .Returns(balanceDto);

        // Act
        var result = await _sut.GetCustomerBalanceAsync(customerId, startDate, endDate);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(balanceDto);
        
        _customerRepositoryMock.Verify(x => x.GetCustomerBalanceAsync(customerId, startDate, endDate), Times.Once);
        _mapperMock.Verify(x => x.Map<CustomerBalanceDto>(balanceModel), Times.Once);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task GetCustomerBalanceAsync_WithInvalidCustomerId_ShouldThrowArgumentException(int invalidId)
    {
        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => 
            _sut.GetCustomerBalanceAsync(invalidId));
        
        exception.Message.Should().Be("Geçersiz müşteri ID'si.");
    }

    [Fact]
    public async Task GetCustomerBalanceAsync_WithInvalidDateRange_ShouldThrowArgumentException()
    {
        // Arrange
        var customerId = 1;
        var startDate = new DateTime(2023, 12, 31);
        var endDate = new DateTime(2023, 1, 1);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => 
            _sut.GetCustomerBalanceAsync(customerId, startDate, endDate));
        
        exception.Message.Should().Be("Başlangıç tarihi bitiş tarihinden büyük olamaz.");
    }

    [Fact]
    public async Task GetMaximumDebtDateAsync_WithValidId_ShouldReturnDate()
    {
        // Arrange
        var customerId = 1;
        var expectedDate = new DateTime(2023, 6, 15);

        _customerRepositoryMock.Setup(x => x.GetCustomerMaxDebtDateAsync(customerId))
            .ReturnsAsync(expectedDate);

        // Act
        var result = await _sut.GetMaximumDebtDateAsync(customerId);

        // Assert
        result.Should().Be(expectedDate);
        
        _customerRepositoryMock.Verify(x => x.GetCustomerMaxDebtDateAsync(customerId), Times.Once);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task GetMaximumDebtDateAsync_WithInvalidId_ShouldThrowArgumentException(int invalidId)
    {
        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => 
            _sut.GetMaximumDebtDateAsync(invalidId));
        
        exception.Message.Should().Be("Geçersiz müşteri ID'si.");
    }

    [Fact]
    public async Task GetCustomerDailyBalancesAsync_WithValidParameters_ShouldReturnMappedBalances()
    {
        // Arrange
        var customerId = 1;
        var dailyBalanceModels = new List<DailyBalanceModel>
        {
            DailyBalanceModel.Create(
                new DateTime(2023, 1, 1),
                1000m,
                new List<TransactionModel>()
            )
        };

        var dailyBalanceDtos = new List<DailyBalanceDto>
        {
            new() { Date = new DateTime(2023, 1, 1), Balance = 1000m }
        };

        _customerRepositoryMock.Setup(x => x.GetCustomerDailyBalancesAsync(customerId, null, null))
            .ReturnsAsync(dailyBalanceModels);

        _mapperMock.Setup(x => x.Map<List<DailyBalanceDto>>(dailyBalanceModels))
            .Returns(dailyBalanceDtos);

        // Act
        var result = await _sut.GetCustomerDailyBalancesAsync(customerId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(dailyBalanceDtos);
        
        _customerRepositoryMock.Verify(x => x.GetCustomerDailyBalancesAsync(customerId, null, null), Times.Once);
        _mapperMock.Verify(x => x.Map<List<DailyBalanceDto>>(dailyBalanceModels), Times.Once);
    }

    [Fact]
    public async Task GetCustomerCurrentBalanceAsync_WithValidId_ShouldReturnBalance()
    {
        // Arrange
        var customerId = 1;
        var expectedBalance = 1500.50m;

        _customerRepositoryMock.Setup(x => x.GetCustomerCurrentBalanceAsync(customerId))
            .ReturnsAsync(expectedBalance);

        // Act
        var result = await _sut.GetCustomerCurrentBalanceAsync(customerId);

        // Assert
        result.Should().Be(expectedBalance);
        
        _customerRepositoryMock.Verify(x => x.GetCustomerCurrentBalanceAsync(customerId), Times.Once);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task GetCustomerCurrentBalanceAsync_WithInvalidId_ShouldThrowArgumentException(int invalidId)
    {
        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => 
            _sut.GetCustomerCurrentBalanceAsync(invalidId));
        
        exception.Message.Should().Be("Geçersiz müşteri ID'si.");
    }
}