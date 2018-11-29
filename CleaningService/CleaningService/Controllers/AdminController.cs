using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CleaningService.Models;
using System.IO;
namespace CleaningService.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        cleaningEntities10 db = new cleaningEntities10();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Adminhome()
        {
            return View();
        }
        public ActionResult adminmsg()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return Redirect("~/Home/Login");

        }
        public ActionResult EmployeeRegistration()
        {
            try
            {
                // var context = new PAJNS.Models.dbcontext();
                var query = from x in db.Employees select x;
                int count = query.Count();
                count++;
                Session["EmployeeId"] = "eid" + count;
                return View();
            }
            catch (Exception)
            {
                //      throw;
            }
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeRegistration(CleaningService.Models.Employee em, FormCollection fr)
        {
            em.EmployeeId = Session["EmployeeId"].ToString();

            if (ModelState.IsValid)
            {
                // var context = new PAJNS.Models.dbcontext();
                db.Employees.Add(em);
                db.SaveChanges();

                TempData["msg"] = "Registration completed successfully.";
                return RedirectToAction("EmployeeRegistration");
            }
            return View();


        }
        public ActionResult EmployeeResignation()
        {
            var abc = (from x in db.Employees where x.EndDate == null select x);
            ViewData["EmployeeId"] = new SelectList(abc, "EmployeeId", "EmployeeId");
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeResignation(FormCollection frm)
        {
            string EmployeeId = frm.Get(0);
            Session["EmployeeId"] = EmployeeId;

            //-------------------------------------------------------------------
            var abc = (from x in db.Employees where x.EndDate == null select x);
            ViewData["EmployeeId"] = new SelectList(abc, "EmployeeId", "EmployeeId");
            var yyy = (from a in db.Employees where a.EmployeeId == EmployeeId select a);
            ViewBag.data = yyy;
            return View(yyy.ToList());

        }
        [HttpPost]
        public ActionResult EmployeeResignation1(FormCollection frm)
        {
            string EmployeeId = Session["EmployeeId"].ToString();
            string enddate = frm.Get("t1");
            string rmks = frm.Get("t2");
            var abc = (from x in db.Employees where x.EmployeeId == EmployeeId select x).FirstOrDefault();
            if (abc != null)
            {
                if (enddate == "" || rmks == "")
                {
                    ViewData["error1"] = "must enter data";
                    var abc1 = (from x in db.Employees where x.EndDate == null select x);
                    ViewData["EmployeeId"] = new SelectList(abc1, "EmployeeId", "EmployeeId");
                    var yyy = (from a in db.Employees where a.EmployeeId == EmployeeId select a);
                    ViewBag.data = yyy;
                    return View("EmployeeResignation");
                }

                else
                {

                    try
                    {
                        abc.EndDate = enddate;
                        abc.Remarks = rmks;
                        var abc1 = (from x in db.Employees where x.EmployeeId == EmployeeId select x).FirstOrDefault();

                        db.SaveChanges();

                        if (abc1 != null)
                        {


                            CleaningService.Models.Employee ee = db.Employees.Find(EmployeeId);
                            db.Employees.Remove(ee);
                            db.SaveChanges();
                            TempData["msg"] = "Employee resignation completed successfully.";
                            return RedirectToAction("EmployeeResignation");


                        }
                        else
                        {
                            TempData["msg"] = "Employee resignation completed successfully.";
                            return RedirectToAction("EmployeeResignation");
                        }
                    }
                    catch (Exception e)
                    {

                        Response.Write(e);
                    }
                }

                return View();
            }
            return View();
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
                            Session["msg"] = "password changed successfully";
                            return Redirect("~/Admin/Adminhome");
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
        public ActionResult servicesentry()
        {
            var query = from x in db.ServiceAvailables select x;
            int count = query.Count();
            count++;
            Session["TypeId"] = "tid" + count;
            string tid = Session["TypeId"].ToString();


            return View();
        }
        [HttpPost]
        public ActionResult servicesentry(CleaningService.Models.ServiceAvailable sa, FormCollection frm, HttpPostedFileBase file)
        {
            //var query = from o in db.ServiceAvailables select o;
            //int count = query.Count();
            //count++;
            //string TypeId = "tid" + count;
            sa.TypeId = Session["TypeId"].ToString();

            sa.Total = sa.MinimumDurationInHours * sa.PricePerHour;
            Session["Total"] = sa.Total;
            sa.Status = "Available";

            string tid = Session["TypeId"].ToString();
            if (file != null)
            {
                int MaxContentLength = 1024 * 1024 * 5; //Size = 4 MB
                string[] AllowedFileExtensions = new string[] { ".jpeg", ".jpg", ".png" };
                if (!AllowedFileExtensions.Contains
     (file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                {
                    ModelState.AddModelError("MenuPicture", "Please upload file of type: " + string.Join(", ", AllowedFileExtensions));
                }
                else if (file.ContentLength > MaxContentLength)
                {
                    ModelState.AddModelError("MenuPicture", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                }
            }

            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    string extn = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    string filename = tid + extn;
                    var path = Path.Combine(Server.MapPath("~/ServicePics"), filename);
                    file.SaveAs(path);

                    filename = "/ServicePics/" + filename;
                    sa.Picture = filename;

                }

                try
                {
                    ServiceAvailable abc = (from x in db.ServiceAvailables where x.TypeName == sa.TypeName select x).SingleOrDefault();
                    if (abc == null)
                    {
                        db.ServiceAvailables.Add(sa);
                        db.SaveChanges();
                        TempData["msg"] = "ServiceDetails Uploaded successfully";
                        return RedirectToAction("servicedetails");
                    }
                    else
                    {
                        TempData["msg"] = "Typename already exist";
                        return RedirectToAction("servicedetails");
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("servicedetails");

                }

            }

            return View();

        }

        public ActionResult serviceavailable_status_update()
        {
            var abc = (from x in db.ServiceAvailables select x);
            ViewData["TypeId"] = new SelectList(abc, "TypeId", "TypeId");

            return View();
        }
        [HttpPost]
        public ActionResult serviceavailable_status_update(FormCollection frm)
        {
            string TypeId = frm.Get(0);
            Session["TypeId"] = TypeId;

            //-------------------------------------------------------------------
            var abc = (from x in db.ServiceAvailables select x);
            ViewData["TypeId"] = new SelectList(abc, "TypeId", "TypeId");
            var yyy = (from a in db.ServiceAvailables where a.TypeId == TypeId select a);
            ViewBag.data = yyy;
            //-------------------------------------------------------------------


            return View();
        }
        [HttpPost]
        public ActionResult serviceavailable_status_update1(FormCollection frm)
        {
            string Status = frm.Get(0);

            string tid = Session["TypeId"].ToString();

            var abc = (from x in db.ServiceAvailables where x.TypeId == tid select x).FirstOrDefault();
            if (abc != null)
            {
                abc.Status = Status;
                db.SaveChanges();
                TempData["msg"] = "Service Status updated successfully.";
                return RedirectToAction("serviceavailable_status_update");
            }
            return View();
        }

        public ActionResult BookingServiceReply(String TypeName)
        {

            var abc = (from x in db.BookingServices where x.Status == "Booking" select x).FirstOrDefault();
            if (abc == null)
            {
                Session["RequestId"] = "";
            }
            else
            {
                var abc1 = (from x in db.BookingServices where x.Status == "Booking" select x);
                ViewBag.data = abc1;
                Session["RequestId"] = "Booking";
            }

            return View();
        }

        public ActionResult BookingConfirmation(String Rid)
        {

            Session["RequestId"] = Rid;
            //  var db = new PAJNS.Models.dbcontext();
            var abc = (from x in db.BookingServices where x.RequestId == Rid && x.Status == "Booking" select x);
            ViewBag.data = abc;

            return View();
        }


        [HttpPost]
        public ActionResult BookingConfirmation(CleaningService.Models.BookingService bs, FormCollection frm, String Rid, String TypeName)
        {
            bs.RequestId = Session["RequestId"].ToString();
            bs.ReplyDate = DateTime.Now.ToShortDateString();
            string status = frm.Get("t1");

            string amount = frm.Get("t2");
            int AdvanceAmount = 0;
            string commentbox = frm.Get("t3");

            if (amount == "")
            {
                ViewData["RequestId"] = new SelectList(db.BookingServices, "RequestId", "Requestid");
                var yy = (from x in db.BookingServices where x.RequestId == Rid select x);
                ViewBag.data = yy;
                var yyy = (from a in db.BookingServices where a.RequestId == Rid select a);
                ViewBag.data1 = yyy;
                ViewData["error"] = "must enter data";
                return View("BookingConfirmation");

            }
            else
            {
                var abc = (from x in db.BookingServices where x.ReplyDate == null select x).FirstOrDefault();

                if (abc != null)
                {
                    try
                    {
                        AdvanceAmount = Convert.ToInt32(frm.Get("t2"));

                        // int advncamount = Int32.Parse(amount);

                        abc.AdvanceAmount = AdvanceAmount;
                        //  abc.PaymentStatus = paymentstatus;
                        abc.Status = status;
                        abc.CommentBox = commentbox;
                        abc.ReplyDate = bs.ReplyDate;

                        db.SaveChanges();

                        TempData["msg"] = "Reply Given.";
                        return RedirectToAction("BookingServiceReply");
                    }
                    catch (Exception e)
                    {

                        Response.Write(e);
                    }
                }


            }
            return View();
        }

        public ActionResult EditBooking(String Rid, CleaningService.Models.Payment py)
        {
            //  string Rid = frm.Get(0);

            Session["RequestId"] = Rid;



            ViewData["RequestId"] = new SelectList(db.BookingServices, "RequestId", "RequestId");
            var bs = (from x in db.BookingServices where x.Status == "Paid" select x);
            ViewBag.data = bs;
            return View();


        }


        public ActionResult EditBookingDetails(string Rid)
        {
            // var db = new PAJNS.Models.dbcontext();
            CleaningService.Models.BookingService bs = db.BookingServices.Find(Rid);
            if (bs == null)
            {
                return HttpNotFound();
            }
            return View(bs);
        }
        [HttpPost]
        public ActionResult EditBookingDetails(CleaningService.Models.BookingService bs, String Rid)
        {
            // string Rid = Session["RequestId"].ToString();
            var abc = (from x in db.BookingServices where x.Status == "Paid" select x).FirstOrDefault();

            if (abc != null)
            {


                abc.Discount = bs.Discount;
                int dis = abc.Discount;
                abc.AdvanceAmount = bs.AdvanceAmount;
                int advamt = abc.AdvanceAmount;
                int add = dis + advamt;
                //abc.StartTime = bs.StartTime;
                //abc.EndTime = bs.EndTime;

                abc.Total = bs.Total;

                int ttl = abc.Total;
                int total = ttl - add;

                abc.Status = bs.Status;
                abc.PaymentAmount = total;

                db.SaveChanges();
                TempData["msg"] = "Booking details Updated Sucessfully. Payment Amount is " + total;
                return RedirectToAction("EditBooking");
            }
            else
            {
                TempData["msg"] = "No Data";
                return Redirect("~/Admin/Adminhome");
            }


            //  return View();


        }

        public ActionResult FeedbackDisplay(string FeedbackId)
        {

            var abc = (from x in db.Feedbacks select x);
            ViewBag.data = abc;
            return View();

        }

        public ActionResult ComplaintReply()
        {

            var yyy = (from a in db.Complaints where a.ReplyDate == null select a);
            ViewBag.data = yyy;
            return View(yyy.ToList());

        }
        public ActionResult Complaint(String ComplaintId)
        {

            var yyy = (from a in db.Complaints where a.ComplaintId == ComplaintId select a);
            ViewBag.data = yyy;
            return View(yyy.ToList());

        }
        [HttpPost]
        public ActionResult Complaint(FormCollection frm, CleaningService.Models.Complaint c, String ComplaintId)
        {
            string Reply = frm.Get("t1");
            string Replydate = frm.Get("t2");
            c.ReplyDate = DateTime.Now.ToShortDateString();
            if (Reply == "")
            {
                ViewData["error"] = "Must enter a reply";
                var abc = (from x in db.Complaints select x);
                ViewData["ComplaintId"] = new SelectList(abc, "ComplaintId", "ComplaintId");
                var yyy = (from a in db.Complaints where a.ComplaintId == ComplaintId select a);
                ViewBag.data = yyy;
                return View("ComplaintReply");
            }

            else
            {
                var abc = (from x in db.Complaints where x.ReplyDate == null select x).FirstOrDefault();

                if (abc != null)
                {
                    try
                    {
                        abc.Reply = Reply;
                        abc.ReplyDate = c.ReplyDate;
                        db.SaveChanges();

                        TempData["msg"] = "Reply given.";
                        return RedirectToAction("ComplaintReply");

                    }
                    catch (Exception e)
                    {

                        Response.Write(e);
                    }
                }

            }
            return View();

        }
        public ActionResult servicedetails()
        {
            var abc = (from x in db.ServiceAvailables select x);
            ViewBag.data = abc;
            return View();
        }
        public ActionResult Edit(string id)

        {
            CleaningService.Models.ServiceAvailable sa = db.ServiceAvailables.Find(id);
            if (sa == null)
            {
                return HttpNotFound();
            }
            return View(sa);
        }
        [HttpPost]
        public ActionResult Edit(CleaningService.Models.ServiceAvailable sa, String id)
        {
            var abc = (from x in db.ServiceAvailables where x.TypeId == id select x).FirstOrDefault();

            if (abc != null)
            {


                abc.MinimumDurationInHours = sa.MinimumDurationInHours;
                abc.PricePerHour = sa.PricePerHour;
                abc.TypeName = sa.TypeName;
                db.SaveChanges();
                TempData["msg"] = "Service details Updated Sucessfully";
                return Redirect("~/Admin/Adminhome");
            }
            else
            {
                TempData["msg"] = "No Data";
                return Redirect("~/Admin/Adminhome");
            }

        }
        public ActionResult Delete(String id)
        {
            CleaningService.Models.ServiceAvailable ee = db.ServiceAvailables.Find(id);
            db.ServiceAvailables.Remove(ee);
            db.SaveChanges();
            TempData["msg"] = "Service Has Been Deleted.";
            return RedirectToAction("servicedetails");

        }

    }
}