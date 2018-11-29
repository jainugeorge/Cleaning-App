namespace CleaningService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [StringLength(50)]
        public string CustomerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Suburb { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
