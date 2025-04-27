using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using newapp_crm.Models;

namespace newapp_crm.Services;

public class DepensesLeadService : IDepensesLeadService
{
    private readonly HttpClient _httpClient;

    public DepensesLeadService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public decimal GetSum(List<DepensesLead> depenses) {
        decimal sum = 0;
        foreach (DepensesLead depense in depenses)
        {
            sum += depense.Amount;
        }
        return sum;
    }
    
    public async Task<List<DepensesLead>> GetAllDepensesAsync()
    {
        try
        {
            // Appel GET à l'API
            HttpResponseMessage response = await _httpClient.GetAsync("api/depenses-lead/all");

            if (response.IsSuccessStatusCode)
            {
                // Lire et désérialiser la réponse JSON
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<DepensesLead>>(json);
            }
            else
            {
                throw new Exception($"Erreur lors de la récupération des données. Statut : {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Une erreur est survenue lors de l'appel à l'API.", ex);
        }
    }
}
