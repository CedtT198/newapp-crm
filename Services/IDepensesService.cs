using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using newapp_crm.Models;

public interface IDepensesService
{
    Task<List<Depenses>> GetAllDepensesAsync();
}