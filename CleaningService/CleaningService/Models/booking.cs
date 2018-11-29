using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CleaningService.Models
{
    public class booking
    {
        public string RequestId { get; set; }
        public string CustomerId { get; set; }
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }
        public string Street { get; set; }
        public string ServiceDate { get; set; }
        public string SubmitDate { get; set; }
        public string Requirements { get; set; }
        public string Status { get; set; }
        public string MinimumDurationInHours { get; set; }
        public int AdvanceAmount { get; set; }
        public string ReplyDate { get; set; }
        public string CommentBox { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int Total { get; set; }
        public int Discount { get; set; }
        public int PaymentAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string Message { get; set; }
        public feedbacks feedbacks { get; set; }
    }
}