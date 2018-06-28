using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using TaxService.Objects;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Configuration;

namespace TaxService
{
    public class TaxService : ITaxService
    {

        public async Task<RateResponseAttributes> GetRate (string apiKey, string apiUrl,string zip)
        {
            //setting the api auth values
            apiKey = ConfigurationSettings.AppSettings["TaxjarApiKey"];
            apiUrl = ConfigurationSettings.AppSettings["TaxjarApiUrl"];
            RateResponse rateResponse = new RateResponse();
            if(string.IsNullOrEmpty(zip))
            {
                return rateResponse.Rate;
            }
            else
            {
                string url = apiUrl + "/rates/" + zip;
                try
                {
                    await Task.Run(async () =>
                    {
                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token token", apiKey);
                            HttpResponseMessage httpResponse = await client.GetAsync(apiUrl);

                            if(httpResponse.IsSuccessStatusCode)
                            {
                                rateResponse = JsonConvert.DeserializeObject<RateResponse>(httpResponse.Content.ToString());
                            }
                        }
                    });
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.Write("Error {0}", ex.Message);
                }
                return rateResponse.Rate;
            }
        }

        public async Task<TaxResponseAttributes> CalculateTax(string apiKey, string apiUrl,Tax taxInfo)
        {
            //setting the api auth values
            apiKey = ConfigurationSettings.AppSettings["TaxjarApiKey"];
            apiUrl = ConfigurationSettings.AppSettings["TaxjarApiUrl"];
            TaxResponse taxResponse = new TaxResponse();

                string url = apiUrl + "/taxes";
                try
                {
                    await Task.Run(async () =>
                    {
                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token token", apiKey);
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                            string jsonContent = JsonConvert.SerializeObject(taxInfo);
                            requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                        HttpResponseMessage httpResponse = await client.SendAsync(requestMessage);
                            if (httpResponse.IsSuccessStatusCode)
                            {
                                taxResponse = JsonConvert.DeserializeObject<TaxResponse>(httpResponse.Content.ToString());
                            }
                        }
                    });
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Write("Error {0}", ex.Message);
                }
            return taxResponse.Tax;

        }
    }
}
