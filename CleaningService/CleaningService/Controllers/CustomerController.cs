using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CleaningService.Models;
namespace CleaningService.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        cleaningEntities10 db = new cleaningEntities10();
       
      
        public ActionResult Customerhome()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return Redirect("~/Home/Login");

        }
        public ActionResult changepassword()
        {
            return View();

        }
        [HttpPost]
        public ActionResult changepassword(FormCollection frm)
        {
            string cpwd = frm.Get("t1");
            string npwd = frm.Get("t2");

            string cnpwd = frm.Get("t3");
            string uname = Session["uname"].ToString();
            if (cpwd == "" && npwd == "" && cnpwd == "")
            {
                TempData["error1"] = "must enter current password";
                TempData["error2"] = "must enter new password";

                TempData["error3"] = "must enter confirm password";
                return View();

            }
            else if (cpwd == "")
            {
                TempData["error1"] = "must enter current password";

            }
            else if (npwd == "")
            {
                TempData["error2"] = "must enter new password";

            }
            else if (cnpwd == "")
            {
                TempData["error3"] = "must enter confirm password";
                return View();

            }
            else
            {
                var abc = (from x in db.Users where x.UserName == uname && x.Password == cpwd select x).FirstOrDefault();
                if (abc != null)
                {
                    try
                    {
                        abc.Password = npwd;
                        if (npwd == cnpwd)
                        {
                            db.SaveChanges();
                            TempData["msg"] = "Password has been changed successfully.";
                            return Redirect("~/Customer/Customerhome");
                          
                        }
                        else
                        {
                            TempData["msg1"] = "password doesnot match";
                        }
                    }
                    catch (Exception ee)
                    {
                        Response.Write(ee);
                    }
                }
                else
                {
                    TempData["msg1"] = "invalid password";
                    return View();
                }

            }
            return View();
        }

        public ActionResult OurServices()
        {
           // var abc = (from x in db.ServiceAvailables where x.Status == "Available" select x).FirstOrDefault();
          //  if (abc == null)
          //  {
          //      Session["RequestId"] = "";
          //  }
           // else
           // {
           // var abc1 = (from x in db.ServiceAvailables where x.Status == "Available" orderby x.TypeName select new { TypeName = x.TypeName }).Distinct();

                var abc = (from x in db.ServiceAvailables where x.Status == "Available" select x);
                ViewBag.data = abc;
                Session["RequestId"] = "Services";
           // }

            return View();
        }
        public ActionResult SelectService()
        {
            try
            {
                // var db = new PAJNS.Models.dbcontext();
                var query = from x in db.BookingServices select x;
                int count = query.Count();
                count++;
                Session["RequestId"] = "Rid" + count;

            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
        [HttpPost]
        public ActionResult SelectService(CleaningService.Models.BookingService bs, String tid, String TypeName,int Total)
        {
            string CustomerId = Session["uname"].ToString();
            bs.CustomerId = CustomerId;
            Session["TypeId"] = tid;
            Session["TypeName"] = TypeName;
       
            bs.Total = Total;
            Session["Total"] = bs.Total;
            bs.SubmitDate = DateTime.Now.ToShortDateString();
            bs.TypeId = Session["TypeId"].ToString();
            //bs.TypeName = Session["TypeName"].ToString();
            Session["AdvanceAmount"] = bs.AdvanceAmount;
            bs.Status = "Booking";
            
            bs.RequestId = Session["RequestId"].ToString();
            if (ModelState.IsValid)
            {
                try
                {
                    //var db = new PAJNS.Models.dbcontext();
                    db.BookingServices.Add(bs);
                    db.SaveChanges();
                    TempData["msg"] = "Thank You for Selecting Our Service. Your Request id=   " + Session["RequestId"];
                    return RedirectToAction("OurServices");
                


                }
                catch (Exception)
                {
                   // throw;
                }
            }
            else
            {
                return View();
            }
            return View();
        }
        public ActionResult CheckBooking(CleaningService.Models.BookingService bs)
        {
            
                /////////
                string uname = Session["uname"].ToString();


                var abc = (from x in db.BookingServices where x.CustomerId == uname && x.Status == "confirmed" select x).FirstOrDefault();

              

                if (abc == null)
                {
                    Session["RequestId"] = "";
                }
                else
                {
                    Session["AdvanceAmount"] = abc.AdvanceAmount;
                    var abc1 = (from x in db.BookingServices where x.CustomerId == uname && x.Status == "confirmed" select x);
                    ViewBag.data = abc1;
                    Session["RequestId"] = "CheckBooking";
                }

             
            return View();

        }
        public ActionResult Payment()
        {

            try
            {

                // var context = new PAJNS.Models.dbcontext();
                var query = from x in db.Payments select x;
                int count = query.Count();
                count++;
                Session["PaymentId"] = "Pid" + count;
                return View();
            }
            catch (Exception)
            {
                throw;
            }




        }
        [HttpPost]
        public ActionResult Payment(CleaningService.Models.Payment pt, String Rid, String Tid, String Cid,CleaningService.Models.VirtualBank vb)
        {
            pt.CustomerId = Cid;
            pt.PaymentId = Session["PaymentId"].ToString();
            pt.RequestId = Rid;
            pt.TypeId = Tid;
            pt.SubmitDate = DateTime.Now.ToShortDateString();
            string advamt = Session["AdvanceAmount"].ToString();
            decimal AdvanceAmount = Convert.ToDecimal(advamt);
            pt.Amount = AdvanceAmount;
            if (ModelState.IsValid)
            {


                var data = db.VirtualBanks.Where(t => t.CardNumber == pt.CardNo && t.CVV == pt.CVV && t.ExpiryMonth == pt.ExpiryMonth && t.ExpiryYear == pt.ExpiryYear).SingleOrDefault();

                if (data != null)
                {
                    if (pt.Amount < data.BalanceAmount)
                    {
                        data.BalanceAmount = data.BalanceAmount - pt.Amount;
                        pt.Status = "Paid";
                        db.Payments.Add(pt);
                        db.SaveChanges();
                        BookingService bk = db.BookingServices.Where(t => t.CustomerId == pt.CustomerId && t.RequestId == pt.RequestId).SingleOrDefault();
                        bk.Status = pt.Status;
                        db.SaveChanges();
                      
                        var admindata = db.VirtualBanks.Where(t => t.CardNumber == "1234567890" && t.CVV == "111").SingleOrDefault();

                        admindata.BalanceAmount += pt.Amount;
                        db.SaveChanges();
                        TempData["msg"] = "Payment done successfully.";
                        return RedirectToAction("OurServices");
                       
                    }
                    else
                    {
                        TempData["msg"] = "Insufficient Balance";
                    }

                }
                else
                {
                    TempData["msg"] = "Invalid Entry";
                }
            }

            else
            {
                return View();

            }
            return View();
        }
        public ActionResult Remove(String Rid)
        {
            string RequestId = Rid;
            CleaningService.Models.BookingService ee = db.BookingServices.Find(Rid);
            db.BookingServices.Remove(ee);
            db.SaveChanges();
            TempData["msg"] = "Your Service Has Been Cancelled.";
            return RedirectToAction("OurServices");
            // return RedirectToAction("OurServices");
        }
        //public ActionResult SearchBookingDetails()
        //{
        //    string uname = Session["uname"].ToString();
        //    var abc = (from x in db.BookingServices where x.CustomerId == uname select x);
        //    ViewData["RequestId"] = new SelectList(abc, "RequestId", "RequestId");

        //    return View();

        //}

        //[HttpPost]
        public ActionResult SearchBookingDetails()
        {

            string uname = Session["uname"].ToString();

            var abc = (from x in db.BookingServices where x.CustomerId == uname && x.Status=="confirmed" select x);
            ViewData["RequestId"] = new SelectList(abc, "RequestId", "RequestId");
         //   string RequestId = frm.Get(0);
            var yyy = (from a in db.BookingServices where a.CustomerId == uname && a.Status=="confirmed"  select a);

            ViewBag.data = yyy;
            return View();

        }
        public ActionResult ServiceFeedBack()
        {
            try
            {
                //  var db = new PAJNS.Models.dbcontext();
                var query = from x in db.Feedbacks select x;
                int count = query.Count();
                count++;
                Session["FeedbackId"] = "Fid" + count;
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult ServiceFeedBack(CleaningService.Models.Feedback fe, FormCollection frm, String Rid)
        {
            if (ModelState.IsValid)
            {
                string CustomerId = Session["uname"].ToString();
                fe.CustomerId = CustomerId;
                fe.RequestId = Rid;
                fe.FeedbackId = Session["FeedbackId"].ToString();
                fe.Date = DateTime.Now.ToShortDateString();


                db.Feedbacks.Add(fe);
                db.SaveChanges();
                  TempData["msg"] = "Feedback submitted successfully.";
                  return RedirectToAction("SearchBookingDetails");

            }
            else
            {
                return View();

            }
        }
        public ActionResult PostComplaint()
        {
            try
            {
                // var db = new PAJNS.Models.dbcontext();

                var query = from x in db.Complaints select x;
                int count = query.Count();
                count++;
                Session["ComplaintId"] = "ComplaintId" + count;

                return View();
            }
            catch (Exception)
            {
                throw;
            }


        }
        [HttpPost]
        public ActionResult PostComplaint(CleaningService.Models.Complaint cmp)
        {
            if (ModelState.IsValid)
            {
                // var db = new PAJNS.Models.dbcontext();
                cmp.ComplaintId = Session["ComplaintId"].ToString();
                cmp.SubmitDate = DateTime.Now.ToShortDateString();
                cmp.UserName = Session["uname"].ToString();

                db.Complaints.Add(cmp);
                db.SaveChanges();
                TempData["msg"] = "Complaints Submitted Successfully.";
                return Redirect("~/Customer/CustomerHome");
            }

            return View();


        }
        //public ActionResult CheckReply()
        //{

        //    string uname = Session["uname"].ToString();



        //    var abc = (from x in db.Complaints where x.UserName == uname select x);


        //    ViewData["ComplaintId"] = new SelectList(abc, "ComplaintId", "ComplaintId");


        //    return View(abc);

        //}
        //[HttpPost]
        public ActionResult CheckReply(string ComplaintId)
        {
            string uname = Session["uname"].ToString();
           
           // Session["ComplaintId"] = ComplaintId;

            var abc = (from x in db.Complaints where x.UserName == uname select x);
            ViewData["ComplaintId"] = new SelectList(abc, "ComplaintId", "ComplaintId");

            var yyy = (from a in db.Complaints where a.UserName == uname select a);

            ViewBag.data = yyy;
            return View(yyy.ToList());


        }
	}
}