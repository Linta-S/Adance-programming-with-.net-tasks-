using Admin.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: AdminHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult home()
        {
             
            TotalProduct();
         
            return View();



        }
        /*public ActionResult totalProduct()
        {
            /* projectdatabaseEntities db = new projectdatabaseEntities();
             var totalList = (from s in db.Systemusers
                               where s.Usertype.Equals("3")
                               select s).Count();
             return View(totalList);

            totalProduct();
            return View();
        }*/

        public void TotalProduct()
        {
            projectdatabaseEntities db = new projectdatabaseEntities();
            ViewBag.Count = db.Products.Count();
            ViewBag.Countp = db.Systemusers.Count();
            ViewBag.Countc = db.Categories.Count();
            ViewBag.Countr = db.Ratings.Count();
            ViewBag.Countrp = db.Returnproducts.Count();
            
        }

        [HttpGet]
        public ActionResult login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult login(Systemuser systemuser)
        {
            if (ModelState.IsValid)
            {
                var db = new projectdatabaseEntities();
                var u = (from e in db.Systemusers
                         where e.U_username.Equals(systemuser.U_username) &&
                         e.U_password.Equals(systemuser.U_password)
                         select e).FirstOrDefault();

                if (u != null)
                {
                     
                    Session["Id"] = u.Id;
                    Session["U_name"] = u.U_name;
                    Session["Usertype"] = u.Usertype;
                    Session["U_phone"] = u.U_phone;
                    Session["U_address"] = u.U_address;
                    Session["U_username"] = u.U_username;
                    Session["U_email"] = u.U_email;
                    Session["U_password"] = u.U_password;

                    if (u.Usertype.Equals("1"))
                    {
                        return RedirectToAction("home");
                    }
                    else if(u.Usertype.Equals("2"))
                    {
                        return RedirectToAction("home");
                    }
                }
            }

            return View();
            }

        public ActionResult Profile(Systemuser systemuser)
        {


            projectdatabaseEntities db = new projectdatabaseEntities();
                var data = db.Systemusers.ToList();
                return View(data);
           

        }



       
        }
}