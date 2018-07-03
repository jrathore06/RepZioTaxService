using System;
namespace TaxService.Interfaces
{
    public interface ITaxCalculator
    {
        string apiKey { get; set; }
        string apiUrl { get; set; }
    }
}
