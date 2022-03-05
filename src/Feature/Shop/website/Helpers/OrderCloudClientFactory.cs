
using System.Configuration;
using OrderCloud.SDK;

namespace BasicCompany.Feature.BasicContent.Helpers
{
    public static class OrderCloudClientFactory
    {
        public static OrderCloudClient CreateClient()
        {
            return new OrderCloudClient(new OrderCloudClientConfig
            {
                ClientId = ConfigurationManager.AppSettings["ClientId"] ?? "2BD024EF-A285-4469-9948-36246083A588",
                ClientSecret = ConfigurationManager.AppSettings["ClientSecret"] ?? "RGPTLzWoUI1ZehGdUlyQlotf1fDrcAJT8nSudZ7NfcZLHEdyNIcGVVnJVZSy",
                Username = ConfigurationManager.AppSettings["Username"] ?? "admin",
                Password = ConfigurationManager.AppSettings["Password"] ?? "123qweASD!@#",
                Roles = new[] { ApiRole.ProductAdmin, ApiRole.OrderAdmin }
            });

        }
    }
}