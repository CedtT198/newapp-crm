using System;
using System.ComponentModel.DataAnnotations;

namespace newapp_crm.Models;

public class TauxAlerte
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTaux { get; set; }
}
