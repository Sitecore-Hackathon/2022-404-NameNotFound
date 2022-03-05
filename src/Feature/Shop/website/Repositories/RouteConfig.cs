using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tshirts.Feature.Shop.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sitecore.DependencyInjection;
using Sitecore.Pipelines;

namespace Tshirts.Feature.Shop.Repositories
{
    public class RouteConfig
    {
        public virtual void Process(PipelineArgs args)
        {
            //RouteTable.Routes.MapRoute("CreateProduct", "CustomApi/Shop/CreateProduct/");
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "CreateProducts",
                url: "api/custom/createprod",
                defaults: new { controller = "Shop", action = "CreateProduct" }
            );
        }
    }
}