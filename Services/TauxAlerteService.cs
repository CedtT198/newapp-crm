using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using newapp_crm.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace newapp_crm.Services;

public class TauxAlerteService : ITauxAlerteService
{
    private readonly HttpClient _httpClient;

    public TauxAlerteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> SaveTauxAlerteAsync(TauxAlerte tauxAlerte)
    {
        try
        {
            var settings = new JsonSerializerSettings
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var jsonData = JsonConvert.SerializeObject(tauxAlerte, settings);
            // string jsonData = JsonSerializer.Serialize(tauxAlerte);
            // Console.WriteLine("data to send");
            // Console.WriteLine(tauxAlerte.Amount);
            // Console.WriteLine(tauxAlerte.DateTaux);
            Console.WriteLine("Json");
            Console.WriteLine(jsonData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/taux-alerte/save", content);
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
}
