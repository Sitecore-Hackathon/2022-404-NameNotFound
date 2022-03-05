using OrderCloud.SDK;

namespace Tshirts.Feature.Shop.Models.Headstart
{
    public class HSOrderWorksheet : OrderWorksheet<HSOrder, HSLineItem, HSShipEstimateResponse, HSOrderCalculateResponse, OrderSubmitResponse, OrderSubmitForApprovalResponse, OrderApprovedResponse>
    {
    }

    public class HSOrderCalculatePayload
    {
        public HSOrderWorksheet OrderWorksheet { get; set; }
        public CheckoutIntegrationConfiguration ConfigData { get; set; }
    }

    public class ShipEstimateResponseXP { }

    public class ShipEstimateXP
    {
    }

    public class ShipMethodXP
    {
    }

    public class HSShipMethod : ShipMethod<ShipMethodXP> { }

    public class HSShipEstimate : ShipEstimate<ShipEstimateXP, HSShipMethod> { }

    public class HSShipEstimateResponse : ShipEstimateResponse<ShipEstimateResponseXP, HSShipEstimate> { }

    public class CheckoutIntegrationConfiguration
    {
    }
}
