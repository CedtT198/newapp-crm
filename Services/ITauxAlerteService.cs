using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using newapp_crm.Models;

namespace newapp_crm.Services;

public interface ITauxAlerteService
{
    Task<string> SaveTauxAlerteAsync(TauxAlerte tauxAlerte);
}

