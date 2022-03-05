using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OrderCloud.SDK;
using System.Collections.Generic;
using Tshirts.Feature.Shop.Helpers;
using Tshirts.Feature.Shop.Models.Headstart.Extended;

namespace Tshirts.Feature.Shop.Models.Headstart
{
    public class SuperHSProduct : IHSObject
    {
        public string ID { get; set; }
        public HSProduct Product { get; set; }
        public PriceSchedule PriceSchedule { get; set; }
        public IList<Spec> Specs { get; set; }
        public IList<HSVariant> Variants { get; set; }
    }

    public class SuperHSMeProduct : IHSObject
    {
        public string ID { get; set; }
        public HSMeProduct Product { get; set; }
        public PriceSchedule PriceSchedule { get; set; }
        public IList<Spec> Specs { get; set; }
        public IList<HSVariant> Variants { get; set; }
    }

    public class PartialHSProduct : PartialProduct<ProductXp>
    {
    }

    public class HSLineItemProduct : LineItemProduct<ProductXp> { }

    public class HSProduct : Product<ProductXp>, IHSObject
    {
    }

    public class HSMeProduct : BuyerProduct<ProductXp, HSPriceSchedule>
    {

    }

    public class HSVariant : Variant<HSVariantXp> { }


    public class ProductXp
    {
        #region DO NOT DELETE
        [OrchestrationIgnore]
        public dynamic IntegrationData { get; set; }
        public Dictionary<string, List<string>> Facets = new Dictionary<string, List<string>>();
        #endregion

        public string Note { get; set; }
        public ProductType ProductType { get; set; }
        public List<ImageAsset> Images { get; set; }
        public string BrandName { get; set; }
        public string Material { get; set; }
        public string Manufacturer { get; set; }
        public CurrencySymbol? Currency { get; set; } = null;
    }

    public class ImageAsset
    {
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
        public List<string> Tags { get; set; }
    };
    
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProductType
    {
        Standard,
        Quote
    }

    public class HSVariantXp
    {
        public string SpecCombo { get; set; }
        public List<HSSpecValue> SpecValues { get; set; }
        public string NewID { get; set; }
        public List<ImageAsset> Images { get; set; }
    }

    public class HSSpecValue
    {
        public string SpecName { get; set; }
        public string SpecOptionValue { get; set; }
        public string PriceMarkup { get; set; }
    }
}
