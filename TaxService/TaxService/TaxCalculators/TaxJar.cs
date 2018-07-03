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
    public class TaxJar : ITaxCalculator
    {
        public string apiKey { get; set; }
        public string apiUrl { get; set; }
        public TaxJar()
        {
            //setting the contstants for the TaxJar api
            apiKey = "5da2f821eee4035db4771edab942a4cc";
            apiUrl = "https://api.taxjar.com/v2/";
        }
        public async Task<RateResponse> GetRate(string zip)
        {
            RateResponse rateResponse = new RateResponse();
                string url = this.apiUrl + "rates/" + zip;
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.apiKey);
                        HttpResponseMessage httpResponse = await client.GetAsync(apiUrl);

                        if (httpResponse.IsSuccessStatusCode)
                        {
                            rateResponse = JsonConvert.DeserializeObject<RateResponse>(httpResponse.Content.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Write("Error {0}", ex.Message);
                }
                return rateResponse;
        }
        public async Task<TaxResponse> CalculateTax(Tax taxInfo)
        {
            TaxResponse taxResponse = new TaxResponse();

            string url = this.apiUrl + "taxes";
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.apiKey);
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                    string jsonContent = JsonConvert.SerializeObject(taxInfo);
                    requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage httpResponse = await client.PostAsync(url, requestMessage.Content);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        taxResponse = JsonConvert.DeserializeObject<TaxResponse>(httpResponse.Content.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write("Error {0}", ex.Message);
            }
            return taxResponse;

        }

    }
}
