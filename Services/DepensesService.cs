using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using newapp_crm.Models;

namespace newapp_crm.Services;

public class DepensesService : IDepensesService
{
    private readonly HttpClient _httpClient;

    public DepensesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    
    public decimal GetSum(List<DepensesPerCustomer> depenses) {
        decimal sum = 0;
        foreach (DepensesPerCustomer depense in depenses)
        {
            sum += depense.Expenses;
        }
        return sum;
    }

    public decimal GetSum(List<Depenses> depenses) {
        decimal sum = 0;
        foreach (Depenses depense in depenses)
        {
            sum += depense.Amount;
        }
        return sum;
    }
    
    
    public async Task<string> UpdateDepensesLeadAsync(DepensesLead depensesLead)
    {
        try
        {
            var settings = new JsonSerializerSettings
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var jsonData = JsonConvert.SerializeObject(depensesLead, settings);
            // string jsonData = JsonSerializer.Serialize(depensesLead);
            Console.WriteLine("data to send");
            Console.WriteLine(depensesLead.Id);
            Console.WriteLine(depensesLead.Amount);
            Console.WriteLine("Json");
            Console.WriteLine(jsonData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/depenses-lead/update", content);
            Console.WriteLine(response);
            Console.WriteLine(response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(); 
            }  
            else
            {
                return $"Error : {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
            }
        }
        catch (Exception ex)
        {
            return $"Exception : {ex.Message}";
        }
    }
    
    public async Task<string> UpdateDepensesTicketAsync(DepensesTicket depensesTicket)
    {
        try
        {
            var settings = new JsonSerializerSettings
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
 
            var jsonData = JsonConvert.SerializeObject(depensesTicket, settings);
            // string jsonData = JsonSerializer.Serialize(depensesTicket);
            Console.WriteLine("data to send");
            Console.WriteLine(depensesTicket.Id);
            Console.WriteLine(depensesTicket.Amount);
            Console.WriteLine("Json");
            Console.WriteLine(jsonData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/depenses-ticket/update", content);
            Console.WriteLine(response);
            Console.WriteLine(response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(); 
            }  
            else
            {
                return $"Error : {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
            }
        }
        catch (Exception ex)
        {
            return $"Exception : {ex.Message}";
        }
    }

    public async Task<string> DeleteLeadAsync(int idLead) 
    {
        try
        { 
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/depenses-ticket/delete/{idLead}");
            Console.WriteLine(response);
            Console.WriteLine(response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(); 
            }  
            else
            {
                return $"Error : {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
            }
        }
        catch (Exception ex)
        {
            return $"Exception : {ex.Message}";
        }
    }
    
    public async Task<string> DeleteTicketAsync(int idTicket)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/depenses-ticket/delete/{idTicket}");
            Console.WriteLine(response);
            Console.WriteLine(response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(); 
            }  
            else
            {
                return $"Error : {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
            }
        }
        catch (Exception ex)
        {
            return $"Exception : {ex.Message}";
        }
    }

    public async Task<List<DepensesPerCustomer>> GetAllDepensesPerCustomerAsync()
    {
        try
        {
            // Appel GET à l'API
            HttpResponseMessage response = await _httpClient.GetAsync("api/depenses/all");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<DepensesPerCustomer> dpc = DepensesPerCustomer.GetExpensesPerCustomer(JsonConvert.DeserializeObject<List<Depenses>>(json));
                return dpc;
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
    
    public async Task<List<Depenses>> GetAllDepensesAsync()
    {
        try
        {
            // Appel GET à l'API
            HttpResponseMessage response = await _httpClient.GetAsync("api/depenses/all");

            if (response.IsSuccessStatusCode)
            {
                // Lire et désérialiser la réponse JSON
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Depenses>>(json);
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
