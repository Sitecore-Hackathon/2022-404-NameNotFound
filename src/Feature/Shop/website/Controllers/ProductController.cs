using BasicCompany.Feature.BasicContent.Repositories;
using System.Web.Mvc;

namespace BasicCompany.Feature.BasicContent.Controllers
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