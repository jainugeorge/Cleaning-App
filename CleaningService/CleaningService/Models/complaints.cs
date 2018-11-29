using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CleaningService.Models
{
    public class complaints
    {
        public string ComplaintId { get; set; }
        public string UserName { get; set; }
        public string Subject { get; set; }
        public string SubmitDate { get; set; }
        public string Complaints { get; set; }
        public string Reply { get; set; }
        public string ReplyDate { get; set; }
        public string Message { get; set; }

    }
}