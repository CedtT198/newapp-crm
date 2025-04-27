using System;
using System.ComponentModel.DataAnnotations;

namespace newapp_crm.Models;

public class DepensesPerCustomer
{
    public int CustomerId { get; set; }
    public decimal Expenses { get; set; }

    public static List<DepensesPerCustomer> GetExpensesPerCustomer(List<Depenses> depenses) {
        var expensesPerCustomer = new List<DepensesPerCustomer>();
        var customers = depenses.Select(d => d.CustomerId).Distinct();
        foreach (var customer in customers) {
            var expenses = depenses.Where(d => d.CustomerId == customer).Sum(d => d.Amount);
            expensesPerCustomer.Add(new DepensesPerCustomer { CustomerId = customer, Expenses = expenses });
        }
        return expensesPerCustomer;
    }
}