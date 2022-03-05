using OrderCloud.SDK;

namespace BasicCompany.Feature.BasicContent.Models.Headstart
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
