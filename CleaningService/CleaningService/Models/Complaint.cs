namespace CleaningService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Complaint
    {
        [StringLength(50)]
        public string ComplaintId { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        public string Subject { get; set; }

        public string SubmitDate { get; set; }

        [Required]
        public string Complaints { get; set; }

        public string Reply { get; set; }

        public string ReplyDate { get; set; }
    }
}
