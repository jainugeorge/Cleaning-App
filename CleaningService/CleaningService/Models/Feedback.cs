namespace CleaningService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Feedback
    {
        [StringLength(50)]
        public string FeedbackId { get; set; }

        [StringLength(50)]
        public string CustomerId { get; set; }

        [StringLength(50)]
        public string RequestId { get; set; }

        [Column("FeedBack")]
        [Required]
        public string FeedBack1 { get; set; }

        public string Date { get; set; }
    }
}
