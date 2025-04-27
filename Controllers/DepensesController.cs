using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using newapp_crm.Models;
using newapp_crm.Services;

namespace newapp_crm.Controllers;

public class DepensesController : Controller
{
    private readonly HttpClient _httpClient;

    public DepensesController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<ActionResult> UpdateDepensesLead(DepensesLead depensesLead)
    {
        try
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8080/") };
            
            var depensesService = new DepensesService(httpClient);
            await depensesService.UpdateDepensesLeadAsync(depensesLead);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return RedirectToAction("Lead");
    }
    
    public async Task<ActionResult> UpdateDepensesTicket(DepensesTicket depensesTicket)
    {
        try
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8080/") };
            
            var depensesService = new DepensesService(httpClient);
            await depensesService.UpdateDepensesTicketAsync(depensesTicket);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return RedirectToAction("Ticket");
    }

    public async Task<ActionResult> DeleteLead(int idLead)
    {
        try
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8080/") };
            
            var depensesService = new DepensesService(httpClient);
            await depensesService.DeleteLeadAsync(idLead);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return RedirectToAction("Ticket");
    }

    public async Task<ActionResult> DeleteTicket(int idTicket)
    {
        try
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8080/") };
            
            var depensesService = new DepensesService(httpClient);
            await depensesService.DeleteTicketAsync(idTicket);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return RedirectToAction("Ticket");
    }

    public async Task<IActionResult> General()
    {
        var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8080/") };
        var depensesService = new DepensesService(httpClient);

        var depenses = await depensesService.GetAllDepensesAsync();
        
        ViewData["depenses"] = depenses;
        ViewData["depensesTicket"] = new DepensesTicket();
        ViewData["depensesLead"] = new DepensesLead();
        return View();
    }

    public async Task<IActionResult> Ticket()
    {
        var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8080/") };
        var depensesService = new DepensesTicketService(httpClient);

        var depenses = await depensesService.GetAllDepensesAsync();
        ViewData["depenses"] = depenses;
        return View(new DepensesTicket()); 
    }

    
    public async Task<IActionResult> Lead()
    {
        var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8080/") };
        var depensesService = new DepensesLeadService(httpClient);

        var depenses = await depensesService.GetAllDepensesAsync();
        ViewData["depenses"] = depenses;
        return View(new DepensesLead());
    }
}