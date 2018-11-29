using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CleaningService.Models
{
    public class Login
    {
        public Boolean status { get; set; }
        public string message { get; set; }

        public custdetails custdetails  { get; set; } 

    }
}