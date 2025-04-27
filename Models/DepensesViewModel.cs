using System;
using System.ComponentModel.DataAnnotations;

namespace newapp_crm.Models;

public class DepensesViewModel
{
    public List<DepensesPerCustomer> Depenses { get; set; }
    public decimal TotalDepensesPerCustomer { get; set;}
    public List<DepensesTicket> DepensesTicket { get; set; }
    public decimal TotalDepensesTicket { get; set;}
    public List<DepensesLead> DepensesLead { get; set; }
    public decimal TotalDepensesLead { get; set;}
}