using OrderCloud.SDK;

namespace BasicCompany.Feature.BasicContent.Models.Headstart
{

    public class HSLineItem : LineItem<LineItemXp, HSLineItemProduct, LineItemVariant, HSAddressBuyer, HSAddressSupplier> { }

    public class HSPartialLineItem : PartialLineItem<LineItemXp, HSLineItemProduct, LineItemVariant, HSAddressBuyer, HSAddressSupplier> { }

    public class LineItemXp
    {
    }
}
