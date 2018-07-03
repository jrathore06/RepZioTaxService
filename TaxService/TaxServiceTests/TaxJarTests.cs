using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxService;
using TaxService.Models;
using System.Threading.Tasks;
namespace TaxServiceTests
{
    [TestClass]
    public class TaxJarTests
    {
        [TestMethod]
        public void GetRate_SantaMonicaZipTest()
        {
            TaxJar taxJar = new TaxJar();
            RateResponse rateResponse = new RateResponse();

            //zip of city of Santa Monica
            string zip = "90404";

            var taxService = new TaxService.TaxService();

            Task.Run(async () =>
            {
                rateResponse.Rate = await taxService.GetRate(taxJar, zip);
            });

            Assert.IsTrue(rateResponse.Rate.City.Equals("SANTA MONICA"));
        }

        [TestMethod]
        public void CalculateTax_SantaMonicaZipTest()
        {
            TaxJar taxJar = new TaxJar();
            TaxResponse taxResponse = new TaxResponse();

            Tax tax = new Tax();

            tax.FromCountry = "US";
            tax.FromZip = "07001";
            tax.FromState = "NJ";
            tax.ToCountry = "US";
            tax.ToZip = "07446";
            tax.ToState = "NJ";
            tax.Amount = 16.5m;
            tax.Shipping = 1.5m;

            TaxLineItem lineItem = new TaxLineItem();
            lineItem.Quantity = 1;
            lineItem.UnitPrice = 15.0m;
            lineItem.ProductTaxCode = "31000";

            tax.LineItems.Add(lineItem);

            var taxService = new TaxService.TaxService();

            Task.Run(async () =>
            {
                taxResponse.Tax = await taxService.CalculateTax(taxJar, tax);
            });

            Assert.IsTrue(taxResponse.OrderTotalAmount == 16.5m);
            Assert.IsTrue(taxResponse.Shipping == 1.5m);
        }
    }
}
