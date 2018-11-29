using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CleaningService.Models
{
    public class CustomerReg
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}