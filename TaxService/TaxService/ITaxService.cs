using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using TaxService.Objects;
namespace TaxService
{
    public interface ITaxService
    {

        Task<RateResponseAttributes> GetRate (string apiKey, string apiUrl, string zip);

        Task<TaxResponseAttributes> CalculateTax(string apiKey, string apiUrl, Tax taxInfo);
    }
}
