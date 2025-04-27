using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using newapp_crm.Models;
using newapp_crm.Services;

namespace newapp_crm.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8080/") };

        var depensesService = new DepensesService(httpClient);
        var depenses = await depensesService.GetAllDepensesPerCustomerAsync();
        var totalDepensesPerCustomer = depensesService.GetSum(depenses);
        
        var depensesTicketService = new DepensesTicketService(httpClient);
        var depensesTicket = await depensesTicketService.GetAllDepensesAsync();
        var totalDepensesTicket = depensesTicketService.GetSum(depensesTicket);
        
        var depensesLeadService = new DepensesLeadService(httpClient);
        var depensesLead = await depensesLeadService.GetAllDepensesAsync();
        var totalDepensesLead = depensesLeadService.GetSum(depensesLead);

        var viewModel = new DepensesViewModel
        {
            Depenses = depenses,
            DepensesTicket = depensesTicket,
            DepensesLead = depensesLead,
            TotalDepensesPerCustomer = totalDepensesPerCustomer,
            TotalDepensesTicket = totalDepensesTicket,
            TotalDepensesLead = totalDepensesLead
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
