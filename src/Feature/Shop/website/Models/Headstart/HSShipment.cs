using OrderCloud.SDK;
using System.Collections.Generic;

namespace BasicCompany.Feature.BasicContent.Models.Headstart
{

    public class SuperHSShipment
    {
        public HSShipment Shipment { get; set; }
        public List<ShipmentItem> ShipmentItems { get; set; }
    }
    
    public class HSShipment : Shipment<ShipmentXp, HSAddressSupplier, HSAddressBuyer>
    {
    }

    public class ShipmentXp
    {
    }
    
    public class HSShipmentWithItems : Shipment
    {
        public List<HSShipmentItemWithLineItem> ShipmentItems { get; set; }
    }

    public class HSShipmentItemWithLineItem : ShipmentItem
    {
        public LineItem LineItem { get; set; }
    }
}
