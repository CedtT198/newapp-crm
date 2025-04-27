using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using newapp_crm.Models;
using newapp_crm.Services;


namespace newapp_crm.Controllers;

public class TauxAlerteController : Controller
{
    private readonly HttpClient _httpClient;

    // Injection de IHttpClientFactory pour créer HttpClient
    public TauxAlerteController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public IActionResult Update()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Update(TauxAlerte tauxAlerte)
    {
        try
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8080/") };
            
            var tauxAlerteService = new TauxAlerteService(httpClient);
            var tauxAlerteReturn = await tauxAlerteService.SaveTauxAlerteAsync(tauxAlerte);
 
            ViewBag.TauxAlerteReturn = tauxAlerteReturn;  
            return View(); 
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error : " + ex.Message);
            ViewBag.TauxAlerteReturn = ex.Message;
            return View(tauxAlerte);
        }
    }
}