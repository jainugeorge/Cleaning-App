using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CleaningService.Models;
namespace CleaningService.Controllers
{
    public class CleaningApiController : Controller
    {
        //
        // GET: /CleaningApi/
        cleaningEntities10 db = new cleaningEntities10();

       

        public JsonResult checklogin(String uname, String Password)
        {
            User u = (from x in db.Users where x.UserName == uname && x.Password == Password select x).FirstOrDefault();
            Customer c = (from x in db.Customers select x).FirstOrDefault();
            Login l=new Login();
            if(u==null)
            {
                l.status=false;
                l.message="Login Failed";
                //Response.Write(uname + password);

            }
            else {
                l.status=true;
                l.message="Login Success";
                l.custdetails = new custdetails();
                l.custdetails.Name=c.Name;
l.custdetails.Role=u.Role;
l.custdetails.PhoneNumber = c.PhoneNumber;
l.custdetails.Email = c.Email;
l.custdetails.City = c.City;
l.custdetails.Suburb = c.Suburb;
l.custdetails.Street = c.Street;

            }
            return Json(l, JsonRequestBehavior.AllowGet);


            }
        public JsonResult customer(string CustomerId)
        {
            Customer c = (from x in db.Customers select x).FirstOrDefault();
            CustomerReg cr = new CustomerReg();
            if(c==null)
            {
                cr.Message = "Registration Failed";
            }
            else
            {
                cr.CustomerId = c.CustomerId;
                cr.Message = "Registration done successfully.";

                cr.Name = c.Name;
                cr.Email = c.Email;
                cr.Password = c.Password;
                cr.ConfirmPassword = c.ConfirmPassword;
                cr.City = c.City;
                cr.Suburb = c.Suburb;
                cr.Street = c.Street;
            }
            return Json(cr, JsonRequestBehavior.AllowGet);

        }
       
        public JsonResult check_email_existance(string CustomerId)
        {
            Customer c = (from x in db.Customers select x).FirstOrDefault();
            CustomerReg cr=new CustomerReg();
            User u = (from x in db.Users where x.UserName == cr.Email select x).FirstOrDefault();
           if(u!=null)
           {
               cr.Message = "Email already exist.";
           }
           else
           {
               cr.CustomerId = c.CustomerId;
               cr.Message = "Registration done successfully.";

               cr.Name = c.Name;
               cr.Email = c.Email;
               cr.Password = c.Password;
               cr.ConfirmPassword = c.ConfirmPassword;
               cr.City = c.City;
               cr.Suburb = c.Suburb;
               cr.Street = c.Street;
           }

           return Json(cr, JsonRequestBehavior.AllowGet);
        }

           public JsonResult serviceavailable(String TypeId)
        {
            ServiceAvailable sa = (from x in db.ServiceAvailables select x).FirstOrDefault();
            service s = new service();
               if(sa==null)
               {
                   s.status = false;
                   s.Message = "No services are available";

               }
               else
               {
                   s.status = true;
                   s.TypeId = sa.TypeId;
                   s.TypeName = sa.TypeName;
                   s.MinimumDurationInHours = sa.MinimumDurationInHours;
                   s.PricePerHour = sa.PricePerHour;
                   s.Total = sa.Total;
                   s.Picture = sa.Picture;
                   s.Status = sa.Status;
               }
               return Json(s, JsonRequestBehavior.AllowGet);

        }
        public JsonResult booking(String RequestId)
           {
               BookingService bs = (from x in db.BookingServices select x).FirstOrDefault();
                           Feedback fb = (from x in db.Feedbacks select x).FirstOrDefault();


               booking b = new booking();
              
            if(bs==null)
            {
                b.Message = "No Bookings";
            }
            else
            {
                b.Message = "Bookings are available";
                b.RequestId = bs.RequestId;
                b.CustomerId = bs.CustomerId;
                b.TypeId = bs.TypeId;
                b.City = bs.City;
                b.Suburb = bs.Suburb;
                b.Street = bs.Street;
                b.ServiceDate = bs.ServiceDate;
                b.Status = bs.Status;
               b.feedbacks = new feedbacks();
                b.feedbacks.FeedBack1 = fb.FeedBack1;
                b.MinimumDurationInHours = bs.MinimumDurationInHours;
                b.AdvanceAmount = bs.AdvanceAmount;
               
                
            }
            return Json(b, JsonRequestBehavior.AllowGet);


           }
        public JsonResult orders(String RequestId)
        {
            BookingService bs = (from x in db.BookingServices select x).FirstOrDefault();
                                       Feedback fb = (from x in db.Feedbacks select x).FirstOrDefault();

            booking b = new booking();
            if (bs == null)
            {
                b.Message = "No Bookings";
            }
            else
            {
                

                b.Message = "Bookings are available";
                b.CustomerId=bs.CustomerId;
                b.RequestId=bs.RequestId;
                b.TypeId = bs.TypeId;
                b.TypeName = bs.TypeName;
                b.Status = bs.Status;
                b.City = bs.City; 
                b.Suburb = bs.Suburb;
                b.Street = bs.Street;
                b.SubmitDate = bs.SubmitDate;
                b.feedbacks = new feedbacks();

                b.feedbacks.FeedbackId = fb.FeedBack1;
                b.MinimumDurationInHours = bs.MinimumDurationInHours;
                b.AdvanceAmount = bs.AdvanceAmount;
            }
            return Json(b,JsonRequestBehavior.AllowGet);
        }
        public JsonResult payment(String RequestId)
        {
            Payment py = (from s in db.Payments select s).FirstOrDefault();
            payments p = new payments();
            if(py==null)
            {
                p.Message = "No payments are done";
            }
            else
            {
                p.PaymentId = py.PaymentId;
                p.CustomerId = py.CustomerId;
               p.RequestId =py.RequestId;
                p.TypeId = py.TypeId;
               
                p.CardNo = py.CardNo;
                p.PaymentMode = py.PaymentMode;
                p.Amount = py.Amount;
                p.Message = "Payment Done";
            }
            return Json(p,JsonRequestBehavior.AllowGet);
        }
        public JsonResult feedback(string RequestId)
        {
            Feedback fb = (from x in db.Feedbacks where x.RequestId == RequestId select x).FirstOrDefault();
            feedbacks f = new feedbacks();
            if(fb==null)
            {
                f.Message = "No feedbacks are Given";
            }
            else
            {
                f.RequestId = fb.RequestId;
                f.FeedBack1 = fb.FeedBack1;
            }
            return Json(f, JsonRequestBehavior.AllowGet);
        }
        public JsonResult employee(String EmployeeId)
        {
            Employee ee = (from x in db.Employees select x).FirstOrDefault();
            employees e = new employees();
            if(ee == null)
            {
                e.Message = "Registration Failed";
            }
            else
            {
                e.EmployeeId = ee.EmployeeId;
                e.Name = ee.Name;
                e.Gender = ee.Gender;
                e.Email = ee.Email;
                e.Address = ee.Address;
                e.DOB = ee.DOB;
                e.Designation = ee.Designation;
                e.PhoneNumber = ee.PhoneNumber;
                e.Experience = ee.Experience;
                e.Salary = ee.Salary;
                e.StartDate = ee.StartDate;
               
                
            }
            return Json(e, JsonRequestBehavior.AllowGet);
        }
        public JsonResult employeeresignation(String EmployeeId)
        {
            Employee ee = (from x in db.Employees select x).FirstOrDefault();
            employees e = new employees();
            e.EmployeeId = ee.EmployeeId;
            e.Name = ee.Name;
            e.EndDate = ee.EndDate;
            e.Remarks = ee.Remarks;
            return Json(e, JsonRequestBehavior.AllowGet);
        }
        public JsonResult complaints(String ComplaintId)
        {
            Complaint ct = (from x in db.Complaints where x.ComplaintId == ComplaintId select x).FirstOrDefault();
            complaints c = new complaints();
            if (ct == null)
            {
                c.Message = "No complaints";

            }
            else
            {
                c.ComplaintId = ct.ComplaintId;
                c.UserName = ct.UserName;
                c.Subject = ct.Subject;
                c.Complaints = ct.Complaints;
                c.SubmitDate = ct.SubmitDate;
            }
            return Json(c, JsonRequestBehavior.AllowGet);
        }
        public JsonResult complaintreply(String ComplaintId)
        {
            Complaint ct = (from x in db.Complaints where x.ComplaintId == ComplaintId select x).FirstOrDefault();
            complaints c = new complaints();
            c.ComplaintId = ct.ComplaintId;
            c.UserName = ct.UserName;
            c.Reply = ct.Reply;
            c.ReplyDate = ct.ReplyDate;
            return Json(c, JsonRequestBehavior.AllowGet);
        }
        }
	}
