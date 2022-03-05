using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Tshirts.Feature.Shop.Models;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Tshirts.Feature.Shop.Controllers
{
    public class ShopController : ApiController
    {

        [System.Web.Http.HttpGet]
        public IHttpActionResult CreateProduct()
        {
            string result = "create single product invoked.";

            try
            {
                var product = new Product();
                //solo para probar
                product.Name = "test prog" + DateTime.Now.Minute.ToString();
                product.Description = "test prog desc";

                CreateSingleProduct(product);
            }
            catch (Exception ex)
            {
                result = "There was an exception when creating a single product. Message: " + ex.ToString();
            }

            return Json(result);
        }

        public bool CreateSingleProduct(Product product)
        {
            Sitecore.Data.Database masterDb = Sitecore.Configuration.Factory.GetDatabase("master");

            //Item parentItem = Sitecore.Context.Item;
            Item productsFolder = masterDb.GetItem(new ID("{02061115-477D-4F9D-9408-91172099F828}"));

            var template = masterDb.GetTemplate(new ID("{5BF87A86-D8C3-454B-870B-EFC3F32BD02E}"));

            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                Item newItem = productsFolder.Add(product.Name, template);
                try
                {
                    if (newItem != null)
                    {
                        newItem.Editing.BeginEdit();
                        newItem["Name"] = product.Name;
                        newItem["Description"] = product.Description;
                        newItem.Editing.EndEdit();
                    }

                    return true;
                }
                catch
                {
                    newItem.Editing.CancelEdit();
                    return false;
                }
            }
        }

        
    }
}