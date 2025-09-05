using Application.DTOs;
using Application.Interfaces;
using Application.Repositories;
using AutoMapper;
using Domain.Exceptions;

namespace Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    /// <summary>
    /// tüm müşterileri getirir
    /// </summary>
    /// <returns></returns>
    public async Task<List<CustomerDto>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return _mapper.Map<List<CustomerDto>>(customers);
    }
    /// <summary>
    /// müşterinin bilgilerini getirir
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<CustomerDto> GetCustomerByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçersiz müşteri ID'si.");

        var customer = await _customerRepository.GetCustomerWithInvoicesAsync(id);
        if (customer == null)
            throw new BusinessException($"ID: {id} olan müşteri bulunamadı.");

        return _mapper.Map<CustomerDto>(customer);
    }
    /// <summary>
    /// müşterinin bakiye bilgilerini getirir
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public async Task<CustomerBalanceDto> GetCustomerBalanceAsync(int customerId, DateTime? startDate = null, DateTime? endDate = null)
    {
        if (customerId <= 0)
            throw new ArgumentException("Geçersiz müşteri ID'si.");

        if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            throw new ArgumentException("Başlangıç tarihi bitiş tarihinden büyük olamaz.");

        var balance = await _customerRepository.GetCustomerBalanceAsync(customerId, startDate, endDate);
        return _mapper.Map<CustomerBalanceDto>(balance);
    }
    /// <summary>
    /// müşterinin maksimum borç tarihini getirir
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    public async Task<DateTime> GetMaximumDebtDateAsync(int customerId)
    {
        if (customerId <= 0)
            throw new ArgumentException("Geçersiz müşteri ID'si.");

        return await _customerRepository.GetCustomerMaxDebtDateAsync(customerId);
    }
    /// <summary>
    /// müşterinin günlük bakiye hareketlerini getirir
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public async Task<List<DailyBalanceDto>> GetCustomerDailyBalancesAsync(int customerId, DateTime? startDate = null, DateTime? endDate = null)
    {
        if (customerId <= 0)
            throw new ArgumentException("Geçersiz müşteri ID'si.");

        if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            throw new ArgumentException("Başlangıç tarihi bitiş tarihinden büyük olamaz.");

        var dailyBalances = await _customerRepository.GetCustomerDailyBalancesAsync(customerId, startDate, endDate);
        return _mapper.Map<List<DailyBalanceDto>>(dailyBalances);
    }
    /// <summary>
    /// müşterinin mevcut bakiyesini getirir
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    public async Task<decimal> GetCustomerCurrentBalanceAsync(int customerId)
    {
        if (customerId <= 0)
            throw new ArgumentException("Geçersiz müşteri ID'si.");

        return await _customerRepository.GetCustomerCurrentBalanceAsync(customerId);
    }
}