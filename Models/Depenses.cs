using System;
using System.ComponentModel.DataAnnotations;

namespace newapp_crm.Models;

public class Depenses
{
    public int Id { get; set; }
    public decimal Amount { get; set; } // Correspond à BigDecimal en Java
    public DateTime DateDepense { get; set; } // Correspond à LocalDate en Java
    public string Source { get; set; }
    public int ReferenceId { get; set; }
    public int CustomerId { get; set; }
    public string Description { get; set; }
    public string Summary { get; set; }
    public bool Confirm { get; set; }
}