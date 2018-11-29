using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CleaningService.Models
{
    public class payments
    {
        public string PaymentId { get; set; }
        public string RequestId { get; set; }
        public string CustomerId { get; set; }
        public string TypeId { get; set; }
        
        public string PaymentMode { get; set; }
        public string CardNo { get; set; }
       
        public Nullable<decimal> Amount { get; set; }
       
        public string Message { get; set; }
    }
}