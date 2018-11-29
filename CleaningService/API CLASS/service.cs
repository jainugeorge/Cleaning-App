using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CleaningService.Models
{
    public class service
    {
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public int MinimumDurationInHours { get; set; }
        public int PricePerHour { get; set; }
        public int Total { get; set; }
        public string Picture { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public Boolean status { get; set; }

    }
}