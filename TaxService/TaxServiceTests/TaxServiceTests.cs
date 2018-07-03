using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxService;
using TaxService.Models;
using System.Threading.Tasks;
namespace TaxServiceTests
{
    [TestClass]
    public class TaxServiceTests
    {
        [TestMethod]
        public void GetRate_EmptyZipTest()
        {
            TaxJar taxJar = new TaxJar();
            RateResponse rateResponse = new RateResponse();

            string zip = "";

            var taxService = new TaxService.TaxService();

            Task.Run(async () =>
            {
                rateResponse.Rate = await taxService.GetRate(taxJar, zip);
            });

            Assert.IsNull(rateResponse.Rate); 
        }
        [TestMethod]
        public void CalculateTax_EmptyToCountryTaxInfoTest()
        {
            TaxJar taxJar = new TaxJar();
            TaxResponse taxResponse = new TaxResponse();

            Tax tax = new Tax();

            tax.ToCountry = "";

            var taxService = new TaxService.TaxService();

            Task.Run(async () =>
            {
                taxResponse.Tax = await taxService.CalculateTax(taxJar, tax);
            });

            Assert.IsNull(taxResponse.Tax);
        }

    }
}
