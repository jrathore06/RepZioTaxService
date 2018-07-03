using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace TaxService.Models
{
    public class Rate
    {

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

    }
}
