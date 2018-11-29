namespace CleaningService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payment
    {
        public string PaymentId { get; set; }

        public string RequestId { get; set; }

        public string CustomerId { get; set; }

        public string TypeId { get; set; }

        public string SubmitDate { get; set; }

        [Required]
        public string PaymentMode { get; set; }

        [Required]
        public string CardNo { get; set; }

        [Required]
        public string CVV { get; set; }

        [Required]
        public string ExpiryMonth { get; set; }

        [Required]
        public string ExpiryYear { get; set; }

        public decimal? Amount { get; set; }

        public string Status { get; set; }
    }
}
