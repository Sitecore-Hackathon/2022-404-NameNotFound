using BasicCompany.Feature.BasicContent.Integrations;
using BasicCompany.Feature.BasicContent.Models.Headstart;
using BasicCompany.Feature.BasicContent.Models.Json;
using BasicCompany.Feature.BasicContent.Repositories;
using OrderCloud.Catalyst;
using OrderCloud.SDK;
using System.Threading.Tasks;
using System.Web.Http;

namespace BasicCompany.Feature.BasicContent.Controllers
{
    [System.Web.Mvc.Route("order")]
    public class OrderController : CatalystController
    {
        [System.Web.Mvc.HttpPost, System.Web.Mvc.Route("create"), OrderCloudUserAuth(ApiRole.Shopper)]
        public async Task<HSOrder> CreateOrder(CreateOrderParamsJson createOrderParams)
        {
            var orderRepository = new OrderRepository();
            return await orderRepository.CreateOrderAsync(createOrderParams, OrderDirection.Outgoing, UserContext);
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.Route("{direction}/{orderId}/submit"), OrderCloudUserAuth(ApiRole.Shopper)]
        public async Task<HSOrder> Submit(OrderDirection direction, string orderId, [FromBody] OrderCloudIntegrationsCreditCardPayment payment)
        {
            var orderRepository = new OrderRepository();
            return await orderRepository.SubmitOrderAsync(orderId, direction, payment, UserContext.AccessToken);
        }
    }
}