using System.Configuration;
using BasicCompany.Feature.BasicContent.Helpers;
using BasicCompany.Feature.BasicContent.Models.Json;
using BasicCompany.Feature.BasicContent.Models.Headstart;
using OrderCloud.Catalyst;
using OrderCloud.SDK;

namespace BasicCompany.Feature.BasicContent.Repositories
{
    public class ProductRepository
    {
        private readonly OrderCloudClient _oc;

        public ProductRepository()
        {
            _oc = OrderCloudClientFactory.CreateClient();
        }

        public BaseResultJson ProductsSync()
        {
            var products = _oc.Products.ListAllAsync<HSProduct>(ConfigurationManager.AppSettings["CatalogId"] ?? "0001").Result;
            foreach (var product in products)
            {
                var pricing = _oc.PriceSchedules.GetAsync(product.DefaultPriceScheduleID).Result;
                var variants = _oc.Products.ListVariantsAsync(product.ID).Result;

                // TODO: Update all items in SiteCore with information from order cloud
            }

            return new BaseResultJson()
            {
                Message = "Successful sync",
                Status = true
            };
        }
    }
}