using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class BilletPayment : Payment
    {
        public BilletPayment(
            string barCode, 
            string billetNumber,
            DateTime paidDate, 
            DateTime expireDate, 
            decimal total, 
            decimal totalPaid, 
            string payer, 
            Document document, 
            Address address, 
            Email email)
            : base (paidDate, expireDate, total, totalPaid, payer, document, address, email)
        {
            BarCode = barCode;
            BilletNumber = billetNumber;
        }

        public string BarCode { get; private set; }
        public string BilletNumber { get; private set; }
    }
}
