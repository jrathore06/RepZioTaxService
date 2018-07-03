using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using TaxService.Models;
using TaxService.Interfaces;

namespace TaxService
{
    public interface ITaxService
    {

        Task<Rate> GetRate (TaxJar TaxCalc, string zip);

        Task<Tax> CalculateTax(TaxJar TaxCalc, Tax taxInfo);
    }
}
