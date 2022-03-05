using Tshirts.Feature.Shop.Repositories;
using System.Web.Mvc;

namespace Tshirts.Feature.Shop.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        [HttpGet, Route("sync")]
        public ActionResult ProductsSync()
        {
            var productRepository = new ProductRepository();
            return Json(productRepository.ProductsSync(), JsonRequestBehavior.AllowGet);
        }
    }
}