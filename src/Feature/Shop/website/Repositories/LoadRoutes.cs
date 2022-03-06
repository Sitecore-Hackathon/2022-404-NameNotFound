using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace Tshirts.Feature.Shop.Repositories
{
    public class LoadRoutes
    {
        public virtual void Process(PipelineArgs args)
        {
            RegisterRoute(RouteTable.Routes);
        }

        protected virtual void RegisterRoute(RouteCollection routes)
        {
            RouteTable.Routes.MapHttpRoute("ProductsSync",
                "api/custom/createprod", /* do not include a forward slash in front of the route */
                new { controller = "Product", action = "ProductsSync" } /* controller name should not have the "Controller" suffix */
            );
        }
    }
}