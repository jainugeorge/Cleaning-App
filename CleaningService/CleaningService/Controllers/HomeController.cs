using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CleaningService.Models;
namespace CleaningService.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        cleaningEntities10 db = new cleaningEntities10();

        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult homemsg()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User u)
        {
            if (ModelState.IsValid)
            {

                
                    Session["uname"] = u.UserName;
                   
                    User abc = (from x in db.Users where x.UserName ==u.UserName && x.Password ==u.Password select x).SingleOrDefault();
                    if (abc != null)
                    {
                        if (abc.Role == "Administrator")
                        {
                            TempData["msg"] = "Welcome Admin";
                            return Redirect("~/Admin/Adminhome");
                        }
                        else if (abc.Role == "Customer")
                        {
                            TempData["msg"] = "Welcome " + u.UserName;
                            return Redirect("~/Customer/Customerhome");
                        }

                        else
                        {
                            try
                            {
                                var query = from x in db.BookingServices select x;
                                int count = query.Count();
                                count++;
                                Session["RequestId"] = "Rid" + count;
                          

                            }
                            catch (Exception)
                            {
                 
                            }
                            return RedirectToAction("Login");
                        }
                    }
                    else
                    {
                        TempData["msg1"] = "Invalid user";
                        return RedirectToAction("Login");
                    }
              
            }
            else
            {
                TempData["msg"] = "Invalid UserName or Password";
                return RedirectToAction("Login");
            }
        }
        public ActionResult CustomerRegistration()
        {
            try
             {
                //   var context = new PAJNS.Models.dbcontext();
                var query = from x in db.Customers select x;
                int count = query.Count();
                count++;
                Session["Customerid"] = "cid" + count;
            }
            catch (Exception)
            {
             //throw;   
            }
            return View();
        }
        [HttpPost]
        public ActionResult CustomerRegistration(CleaningService.Models.Customer cu,CleaningService.Models.User u)
        {
            cu.CustomerId = Session["CustomerId"].ToString();
            Session["Email"] = cu.Email;
            User abc=(from s in db.Users where s.UserName==cu.Email select s).FirstOrDefault();
            if (abc == null)
            {


                if (ModelState.IsValid)
                {
                    try
                    {


                        if (cu.Password == cu.ConfirmPassword)
                        {
                            db.Customers.Add(cu);
                            db.SaveChanges();
                            CleaningService.Models.User usr = new CleaningService.Models.User();

                            Session["uname"] = Session["Email"].ToString();
                            usr.UserName = Session["Email"].ToString();
                            usr.Password = cu.Password;
                            usr.Role = "Customer";
                            db.Users.Add(usr);
                            db.SaveChanges();

                            TempData["msg"] = "Welcome to our Website. Your Customer id=   " + Session["Customerid"];
                            return Redirect("~/Customer/Customerhome");
                        }

                        else
                        {
                            TempData["msg"] = "PASSWORD MISMATCH";
                            return Redirect("~/Home/CustomerRegistration");
                        }

                    }
                    catch (Exception)
                    {
                        //   throw;
                    }
                }
            }
            else
            {
                TempData["msg"] = "Email Already exists";
                return Redirect("~/Home/CustomerRegistration");
            }
           
                return View();
           
        } 
        public ActionResult FAQS()
        {
            return View("../Home/faqs");
        }
        
	}
}