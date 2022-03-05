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
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisterRoute(RouteTable.Routes);
        }

        protected virtual void RegisterRoute(RouteCollection routes)
        {
            RouteTable.Routes.MapHttpRoute("CreateProduct",
                "api/custom/createprod", /* do not include a forward slash in front of the route */
                new { controller = "Shop", action = "CreateProduct" } /* controller name should not have the "Controller" suffix */
            );
        }
    }
}