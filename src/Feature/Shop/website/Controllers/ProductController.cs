using System.Threading.Tasks;
using System.Web.Http;
using Tshirts.Feature.Shop.Repositories;
using System.Web.Mvc;

namespace Tshirts.Feature.Shop.Controllers
{
    [System.Web.Mvc.Route("product")]
    public class ProductController : ApiController
    {
        [System.Web.Http.HttpGet, System.Web.Mvc.Route("sync")]
        public async Task<IHttpActionResult> ProductsSync()
        {
            var productRepository = new ProductRepository();
            return Json(await productRepository.ProductsSync());
        }
    }
}