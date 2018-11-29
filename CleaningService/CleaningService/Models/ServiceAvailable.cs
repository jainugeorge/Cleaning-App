namespace CleaningService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ServiceAvailable
    {
        [Key]
        public string TypeId { get; set; }

        [Required]
        public string TypeName { get; set; }

        public int MinimumDurationInHours { get; set; }

        public int PricePerHour { get; set; }

        public int Total { get; set; }

        public string Picture { get; set; }

        public string Status { get; set; }
    }
}
