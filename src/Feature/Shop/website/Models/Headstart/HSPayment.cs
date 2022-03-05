using OrderCloud.SDK;

namespace Tshirts.Feature.Shop.Models.Headstart
{

    public class HSPayment : Payment<PaymentXP, HSPaymentTransaction>
    {
    }


    public class HSPaymentTransaction : PaymentTransaction<TransactionXP>
    {
    }


    public class PaymentXP
    {
    }


    public class TransactionXP
    {
    }
}
