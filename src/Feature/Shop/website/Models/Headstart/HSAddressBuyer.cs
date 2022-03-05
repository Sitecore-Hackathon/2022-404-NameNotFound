using OrderCloud.SDK;

namespace Tshirts.Feature.Shop.Models.Headstart
{
    public class HSAddressBuyer : Address<BuyerAddressXP>, IHSObject
    {
    }

    public class HSAddressMeBuyer : BuyerAddress<BuyerAddressXP>, IHSObject
    {
    }

    public class BuyerAddressXP
    {
      
    }
}
