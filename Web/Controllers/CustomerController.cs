using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers;

public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    /// <summary>
    /// Ana sayfa - Müşteri bakiye seyri
    /// </summary>
    public async Task<IActionResult> Index(int? customerId, DateTime? startDate, DateTime? endDate)
    {
        var viewModel = new CustomerBalanceViewModel();


        // Tüm müşterileri getir
        var customers = await _customerService.GetAllCustomersAsync();
        viewModel.Customers = customers;

        if (customerId.HasValue)
        {
            viewModel.CustomerId = customerId.Value;

            // Seçili müşteri bilgilerini getir
            var selectedCustomer = await _customerService.GetCustomerByIdAsync(customerId.Value);
            viewModel.CustomerTitle = selectedCustomer.Title;

            // Bakiye bilgilerini getir
            viewModel.BalanceData = await _customerService.GetCustomerBalanceAsync(customerId.Value, startDate, endDate);
            viewModel.StartDate = startDate;
            viewModel.EndDate = endDate;
        }

        return View(viewModel);
    }

    /// <summary>
    /// Müşteri detay sayfası
    /// </summary>
    public async Task<IActionResult> Details(int id)
    {

        var customer = await _customerService.GetCustomerByIdAsync(id);
        var balance = await _customerService.GetCustomerBalanceAsync(id);
        var currentBalance = await _customerService.GetCustomerCurrentBalanceAsync(id);

        var viewModel = new CustomerBalanceViewModel
        {
            CustomerId = id,
            CustomerTitle = customer.Title,
            BalanceData = balance
        };

        return View(viewModel);
    }

    /// <summary>
    /// Hata sayfası
    /// </summary>
    public IActionResult Error(string? message = null)
    {
        ViewBag.ErrorMessage = message ?? "Bir hata oluştu.";
        return View();
    }
}