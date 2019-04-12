using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStorePayment.Models
{
    public class PaymentDetails
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public string ExpiryDate { get; set; }
        //public decimal AmountOfMoney { get; set; }
    }
}
