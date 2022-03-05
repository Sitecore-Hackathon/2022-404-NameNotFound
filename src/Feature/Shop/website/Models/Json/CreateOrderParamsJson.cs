using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Tshirts.Feature.Shop.Models.Json
{
    [Serializable]
    public class CreateOrderParamsJson
    {
        [JsonProperty("ShippingAddress")]
        public UserAddress ShippingAddress { get; set; }

        [JsonProperty("BillingAddress")]
        public UserAddress BillingAddress { get; set; }

        [JsonProperty("CreditCard")]
        public UserCreditCard CreditCard { get; set; }

        [JsonProperty("Order")]
        public UserOrder Order { get; set; }
    }

    [Serializable]
    public class UserAddress
    {
        [JsonProperty("Shipping")]
        public bool Shipping { get; set; }

        [JsonProperty("Billing")]
        public bool Billing { get; set; }

        [JsonProperty("AddressName")]
        public string AddressName { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Street1")]
        public string Street1 { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("Zip")]
        public string Zip { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }
    }

    [Serializable]
    public class UserCreditCard
    {
        [JsonProperty("CardType")]
        public string CardType { get; set; }

        [JsonProperty("PartialAccountNumber")]
        public string PartialAccountNumber { get; set; }

        [JsonProperty("CardholderName")]
        public string CardholderName { get; set; }

        [JsonProperty("ExpirationDate")]
        public DateTime ExpirationDate { get; set; }
    }

    [Serializable]
    public class UserOrder
    {
        [JsonProperty("ShippingCost")]
        public decimal ShippingCost { get; set; }

        [JsonProperty("ShippingAddressId")]
        public string ShippingAddressId { get; set; }

        [JsonProperty("BillingAddressId")]
        public string BillingAddressId { get; set; }

        [JsonProperty("CreditCardId")]
        public string CreditCardId { get; set; }

        [JsonProperty("OrderItems")]
        public List<UserOrderItem> OrderItems { get; set; }
    }

    [Serializable]
    public class UserOrderItem
    {
        [JsonProperty("ProductId")]
        public string ProductId { get; set; }

        [JsonProperty("Quantity")]
        public int Quantity { get; set; }

        [JsonProperty("Color")]
        public string Color { get; set; }

        [JsonProperty("Size")]
        public string Size { get; set; }
    }
}