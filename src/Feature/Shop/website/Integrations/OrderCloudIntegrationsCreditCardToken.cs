using OrderCloud.SDK;
using System.ComponentModel.DataAnnotations;

namespace Tshirts.Feature.Shop.Integrations
{
    public class OrderCloudIntegrationsCreditCardToken
    {
        public string AccountNumber { get; set; }
        [OrderCloud.SDK.Required]
        [MinLength(4, ErrorMessage = "Invalid expiration date format: MMYY or MMYYYY")]
        [MaxLength(6, ErrorMessage = "Invalid expiration date format: MMYY or MMYYYY")]
        public string ExpirationDate { get; set; }
        public string CardholderName { get; set; }
        public string CardType { get; set; }
        [OrderCloud.SDK.Required]
        public Address CCBillingAddress { get; set; }
    }


}
