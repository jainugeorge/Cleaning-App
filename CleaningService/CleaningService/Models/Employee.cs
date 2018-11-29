namespace CleaningService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        [StringLength(50)]
        public string EmployeeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string DOB { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Experience { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        public string Salary { get; set; }

        [Required]
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Remarks { get; set; }
    }
}
