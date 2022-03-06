
using System.Configuration;
using OrderCloud.SDK;

namespace Tshirts.Feature.Shop.Helpers
{
    public static class OrderCloudClientFactory
    {
        public static OrderCloudClient CreateClient()
        {
            return new OrderCloudClient(new OrderCloudClientConfig
            {
                ClientId = ConfigurationManager.AppSettings["ClientId"] ?? "2BD024EF-A285-4469-9948-36246083A588",
                ClientSecret = ConfigurationManager.AppSettings["ClientSecret"] ?? "RGPTLzWoUI1ZehGdUlyQlotf1fDrcAJT8nSudZ7NfcZLHEdyNIcGVVnJVZSy",
                ApiUrl = "https://sandboxapi.ordercloud.io",
                AuthUrl = "https://sandboxapi.ordercloud.io",
                Roles = new[] { ApiRole.FullAccess }
            });

        }
    }
}