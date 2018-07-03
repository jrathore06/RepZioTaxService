using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using TaxService.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Configuration;
using TaxService.Interfaces;
namespace TaxService
{
    public class TaxService : ITaxService
    {
        
        public async Task<Rate> GetRate (TaxJar taxJar,string zip)
        {
            //setting the api auth values

            RateResponse rateResponse = new RateResponse();

            if(string.IsNullOrEmpty(zip))
            {
                rateResponse.Rate = null;
            }
            else
            {
                try
                {
                    await Task.Run(async () =>
                    {
                        rateResponse = await taxJar.GetRate(zip);
                    });
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.Write("Error {0}", ex.Message);
                }
            }
            return rateResponse.Rate;
        }

        public async Task<Tax> CalculateTax(TaxJar taxJar,Tax taxInfo)
        {
            //setting the api auth values

            TaxResponse taxResponse = new TaxResponse();
            if(taxInfo.ToCountry.Equals(""))
            {
                taxResponse.Tax = null;
            }
            else
            {
                try
                {
                    await Task.Run(async () =>
                    {
                        taxResponse = await taxJar.CalculateTax(taxInfo);
                    });
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Write("Error {0}", ex.Message);
                }
            }
                
            return taxResponse.Tax;

        }
    }
}
