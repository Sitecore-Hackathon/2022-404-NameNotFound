using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Tshirts.Feature.Shop.Helpers;
using Tshirts.Feature.Shop.Models.Json;
using Tshirts.Feature.Shop.Models.Headstart;
using OrderCloud.Catalyst;
using OrderCloud.SDK;
using Sitecore.Data;
using Sitecore.Data.Items;
using Product = Tshirts.Feature.Shop.Models.Product;

namespace Tshirts.Feature.Shop.Repositories
{
    public class ProductRepository
    {
        private readonly OrderCloudClient _oc;

        public ProductRepository()
        {
            _oc = OrderCloudClientFactory.CreateClient();
        }

        public async Task<BaseResultJson> ProductsSync()
        {
            try
            {
                var products = await _oc.Products.ListAllAsync<HSProduct>(ConfigurationManager.AppSettings["CatalogId"] ?? "0001");
                foreach (var product in products)
                {
                    var pricing = await _oc.PriceSchedules.GetAsync(product.DefaultPriceScheduleID);
                    var variants = await _oc.Products.ListVariantsAsync(product.ID);
                    var specs = await _oc.Products.ListSpecsAsync(product.ID);

                    CreateSingleProduct(product, pricing, variants, specs);
                }

                return new BaseResultJson()
                {
                    Message = "Successful sync",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            
        }

        public bool CreateSingleProduct(HSProduct product, PriceSchedule pricing, ListPage<Variant> variants, ListPage<Spec> specs)
        {
            Sitecore.Data.Database masterDb = Sitecore.Configuration.Factory.GetDatabase("master");

            Item productsFolder = masterDb.GetItem(new ID("{02061115-477D-4F9D-9408-91172099F828}"));

            var template = masterDb.GetTemplate(new ID("{5BF87A86-D8C3-454B-870B-EFC3F32BD02E}"));

            

            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                var existingItem = productsFolder.Children.FirstOrDefault(x => x.Name == product.ID);
                
                try
                {
                    if (existingItem != null)
                    {
                        existingItem.Editing.BeginEdit();
                        existingItem["Id"] = product.ID;
                        existingItem["Name"] = product.Name;
                        existingItem["Brand Name"] = product.xp.BrandName;
                        existingItem["Color"] = string.Join(",", specs.Items.Single(x => x.ID == $"{product.ID}Color").Options.Select(x => x.ID));
                        existingItem["Size"] = string.Join(",", specs.Items.Single(x => x.ID == $"{product.ID}Size").Options.Select(x => x.ID));
                        existingItem["Description"] = product.Description;
                        existingItem["Material"] = product.xp.Material;
                        existingItem["Manufacturer"] = product.xp.Manufacturer;
                        existingItem["Price"] = pricing.PriceBreaks.First().Price.ToString(CultureInfo.InvariantCulture);
                        existingItem["Images"] = string.Join("|", product.xp.Images.Select(x => $"{x.Tags.First()};{x.Url}"));
                        existingItem["Inventory"] = product.Inventory.QuantityAvailable.ToString();
                        existingItem.Editing.EndEdit();
                    }
                    else
                    {
                        Item newItem = productsFolder.Add(product.ID, template);
                        if (newItem != null)
                        {
                            newItem.Editing.BeginEdit();
                            newItem["Id"] = product.ID;
                            newItem["Name"] = product.Name;
                            newItem["Brand Name"] = product.xp.BrandName;
                            newItem["Color"] = string.Join(",",
                                specs.Items.Single(x => x.ID == $"{product.ID}Color").Options.Select(x => x.ID));
                            newItem["Size"] = string.Join(",",
                                specs.Items.Single(x => x.ID == $"{product.ID}Size").Options.Select(x => x.ID));
                            newItem["Description"] = product.Description;
                            newItem["Material"] = product.xp.Material;
                            newItem["Manufacturer"] = product.xp.Manufacturer;
                            newItem["Price"] = pricing.PriceBreaks.First().Price.ToString(CultureInfo.InvariantCulture);
                            newItem["Images"] = string.Join("|", product.xp.Images.Select(x => $"{x.Tags.First()};{x.Url}"));
                            newItem["Inventory"] = product.Inventory.QuantityAvailable.ToString();
                            newItem.Editing.EndEdit();
                        }
                    }

                    return true;
                }
                catch
                {
                    //newItem.Editing.CancelEdit();
                    return false;
                }
            }
        }
    }
}