using Tshirts.Feature.Shop.Integrations;
using Tshirts.Feature.Shop.Models.Headstart;
using Tshirts.Feature.Shop.Models.Json;
using OrderCloud.Catalyst;
using OrderCloud.SDK;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tshirts.Feature.Shop.Helpers;

namespace Tshirts.Feature.Shop.Repositories
{
    public class OrderRepository
    {
        private readonly OrderCloudClient _oc;

        public OrderRepository()
        {
            _oc = OrderCloudClientFactory.CreateClient();
        }

        public async Task<HSOrder> CreateOrderAsync(CreateOrderParamsJson createOrderParams, OrderDirection outgoing, DecodedToken userContext)
        {
            try
            {
                // Create billing address
                var billingAddress = new BuyerAddress() { ID = createOrderParams.Order.BillingAddressId };
                if (string.IsNullOrEmpty(createOrderParams.Order.BillingAddressId) &&
                    createOrderParams.BillingAddress != null)
                {
                    billingAddress = await _oc.Me.CreateAddressAsync(new BuyerAddress()
                    {
                        AddressName = createOrderParams.BillingAddress.AddressName,
                        City = createOrderParams.BillingAddress.City,
                        Country = createOrderParams.BillingAddress.Country,
                        State = createOrderParams.BillingAddress.State,
                        DateCreated = DateTime.Now,
                        Billing = createOrderParams.BillingAddress.Billing,
                        Shipping = createOrderParams.BillingAddress.Shipping,
                        FirstName = createOrderParams.BillingAddress.FirstName,
                        LastName = createOrderParams.BillingAddress.LastName,
                        Street1 = createOrderParams.BillingAddress.Street1,
                        Phone = createOrderParams.BillingAddress.Phone,
                        Zip = createOrderParams.BillingAddress.Zip
                    }, userContext.AccessToken);
                }

                // Create shipping address
                var shippingAddress = new BuyerAddress() { ID = createOrderParams.Order.ShippingAddressId };
                if (string.IsNullOrEmpty(createOrderParams.Order.ShippingAddressId) &&
                    createOrderParams.ShippingAddress != null)
                {
                    shippingAddress = await _oc.Me.CreateAddressAsync(new BuyerAddress()
                    {
                        AddressName = createOrderParams.ShippingAddress.AddressName,
                        City = createOrderParams.ShippingAddress.City,
                        Country = createOrderParams.ShippingAddress.Country,
                        State = createOrderParams.ShippingAddress.State,
                        DateCreated = DateTime.Now,
                        Billing = createOrderParams.ShippingAddress.Billing,
                        Shipping = createOrderParams.ShippingAddress.Shipping,
                        FirstName = createOrderParams.ShippingAddress.FirstName,
                        LastName = createOrderParams.ShippingAddress.LastName,
                        Street1 = createOrderParams.ShippingAddress.Street1,
                        Phone = createOrderParams.ShippingAddress.Phone,
                        Zip = createOrderParams.ShippingAddress.Zip
                    }, userContext.AccessToken);
                }

                var sameAddress = billingAddress.Billing && billingAddress.Shipping ||
                                   shippingAddress.Billing && shippingAddress.Shipping ||
                                   billingAddress.ID == shippingAddress.ID;

                // Create card address
                var card = new BuyerCreditCard() { ID = createOrderParams.Order.CreditCardId };
                if (string.IsNullOrEmpty(createOrderParams.Order.CreditCardId) &&
                    createOrderParams.CreditCard != null)
                {
                    card = await _oc.Me.CreateCreditCardAsync(new BuyerCreditCard()
                    {
                        CardType = createOrderParams.CreditCard.CardType,
                        CardholderName = createOrderParams.CreditCard.CardholderName,
                        ExpirationDate = createOrderParams.CreditCard.ExpirationDate,
                        PartialAccountNumber = createOrderParams.CreditCard.PartialAccountNumber,
                        DateCreated = DateTime.Now,
                        Token = Guid.NewGuid().ToString()
                    }, userContext.AccessToken);
                }

                // Create order
                var order = await _oc.Orders.CreateAsync<HSOrder>(OrderDirection.Outgoing, new Order()
                {
                    BillingAddressID = billingAddress.ID,
                    ShippingAddressID = shippingAddress.ID,
                    ShippingCost = createOrderParams.Order.ShippingCost
                }, userContext.AccessToken);

                // Create order items
                var lineItems = new List<LineItem>();
                foreach (var orderItem in createOrderParams.Order.OrderItems)
                {
                    var variant = await _oc.Products.ListVariantsAsync(orderItem.ProductId);
                    var lineItem = await _oc.LineItems.CreateAsync(OrderDirection.Outgoing, order.ID, new LineItem()
                    {
                        ProductID = orderItem.ProductId,
                        Quantity = orderItem.Quantity,
                        Specs = variant.Items
                            .SingleOrDefault(x => x.ID == $"{orderItem.ProductId}-{orderItem.Color}-{orderItem.Size}")?.Specs
                            .Select(z => new LineItemSpec()
                            {
                                SpecID = z.SpecID,
                                Name = z.Name,
                                OptionID = z.OptionID,
                                Value = z.Value,
                                PriceMarkupType = z.PriceMarkupType,
                                PriceMarkup = z.PriceMarkup
                            }).ToList()
                    }, userContext.AccessToken);

                    lineItems.Add(lineItem);
                }

                // Create order payment
                var orderPayment = await _oc.Payments.CreateAsync(OrderDirection.Outgoing, order.ID, new Payment()
                {
                    Type = PaymentType.CreditCard,
                    CreditCardID = card.ID,
                    SpendingAccountID = null,
                    Description = "Payment for User's Order",
                    Amount = null,
                    Accepted = false
                }, userContext.AccessToken);

                // Create payment card transactions
                var orderPaymentAuthorizeTransaction = await _oc.Payments.CreateTransactionAsync(OrderDirection.Incoming,
                    order.ID, orderPayment.ID, new PaymentTransaction()
                    {
                        ID = "authorize",
                        Type = "CreditCard",
                        DateExecuted = DateTime.Now,
                        Succeeded = true,
                        ResultCode = "I00001",
                        ResultMessage = "Successful"
                    });

                var orderPaymentCaptureTransaction = await _oc.Payments.CreateTransactionAsync(OrderDirection.Incoming,
                    order.ID, orderPayment.ID, new PaymentTransaction()
                    {
                        ID = "capture",
                        Type = "CreditCard",
                        DateExecuted = DateTime.Now,
                        Succeeded = true,
                        ResultCode = "I00001",
                        ResultMessage = "Successful"
                    });

                // Update payment
                orderPayment = await _oc.Payments.PatchAsync(OrderDirection.Incoming, order.ID, orderPayment.ID,
                    new PartialPayment()
                    {
                        Accepted = true
                    });

                // Submit order
                var submittedOrder = await _oc.Orders.SubmitAsync<HSOrder>(OrderDirection.Outgoing, order.ID, userContext.AccessToken);

                // Create shipment
                var shipment = await _oc.Shipments.CreateAsync(new Shipment()
                {
                    BuyerID = ConfigurationManager.AppSettings["BuyerId"] ?? "0001",
                    Shipper = "UPS",
                    DateShipped = DateTime.Now,
                    DateDelivered = DateTime.Now.AddDays(2),
                    TrackingNumber = Guid.NewGuid().ToString(),
                    ToAddressID = shippingAddress.ID,
                    Cost = 5
                });

                // Create shipment line items
                foreach (var lineItem in lineItems)
                {
                    var shipmentLineItem = await _oc.Shipments.SaveItemAsync(shipment.ID, new ShipmentItem()
                    {
                        OrderID = submittedOrder.ID,
                        LineItemID = lineItem.ID,
                        QuantityShipped = lineItem.Quantity
                    });
                }

                return submittedOrder;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<HSOrder> SubmitOrderAsync(string orderId, OrderDirection direction, OrderCloudIntegrationsCreditCardPayment payment, string userToken)
        {
            var worksheet = await _oc.IntegrationEvents.GetWorksheetAsync<HSOrderWorksheet>(OrderDirection.Incoming, orderId);
            await ValidateOrderAsync(worksheet, payment, userToken);

            // If Credit Card info is null, payment is a Purchase Order, thus skip CC validation
            if (payment.CreditCardDetails != null || payment.CreditCardID != null)
            {
                payment.OrderID = orderId;
            }

            return await _oc.Orders.SubmitAsync<HSOrder>(direction, orderId, userToken);
        }

        private async Task ValidateOrderAsync(HSOrderWorksheet worksheet, OrderCloudIntegrationsCreditCardPayment payment, string userToken)
        {
            Require.That(
                !worksheet.Order.IsSubmitted,
                new ErrorCode("OrderSubmit.AlreadySubmitted", "Order has already been submitted")
            );

            var shipMethodsWithoutSelections = worksheet.ShipEstimateResponse?.ShipEstimates?.Where(estimate => estimate.SelectedShipMethodID == null);
            Require.That(
                worksheet.ShipEstimateResponse != null &&
                !(shipMethodsWithoutSelections ?? Array.Empty<HSShipEstimate>()).Any(),
                new ErrorCode("OrderSubmit.MissingShippingSelections", "All shipments on an order must have a selection"), shipMethodsWithoutSelections
                );

            Require.That(
                !worksheet.LineItems.Any() || payment != null,
                new ErrorCode("OrderSubmit.MissingPayment", "Order contains standard line items and must include credit card payment details"),
                worksheet.LineItems
            );
            var lineItemsInactive = await GetInactiveLineItems(worksheet, userToken);
            Require.That(
                !lineItemsInactive.Any(),
                new ErrorCode("OrderSubmit.InvalidProducts", "Order contains line items for products that are inactive"), lineItemsInactive
            );

            try
            {
                // ordercloud validates the same stuff that would be checked on order submit
                await _oc.Orders.ValidateAsync(OrderDirection.Incoming, worksheet.Order.ID);
            }
            catch (OrderCloudException exp)
            {
                // credit card payments aren't accepted yet, so ignore this error for now
                // we'll accept the payment once the credit card auth goes through (before order submit)
                var errors = exp.Errors.Where(ex => ex.ErrorCode != "Order.CannotSubmitWithUnaccceptedPayments");
                if (errors.Any())
                {
                    throw new CatalystBaseException("OrderSubmit.OrderCloudValidationError", "Failed ordercloud validation, see Data for details", errors);
                }
            }
        }

        private async Task<List<HSLineItem>> GetInactiveLineItems(HSOrderWorksheet worksheet, string userToken)
        {
            var inactiveLineItems = new List<HSLineItem>();
            foreach (var lineItem in worksheet.LineItems)
            {
                try
                {
                    await _oc.Me.GetProductAsync(lineItem.ProductID, sellerID: "BNd2ww6Orcqxr6xG", accessToken: userToken);
                }
                catch (OrderCloudException ex) when (ex.HttpStatus == HttpStatusCode.NotFound)
                {
                    inactiveLineItems.Add(lineItem);
                }
            }
            return inactiveLineItems;
        }
    }
}