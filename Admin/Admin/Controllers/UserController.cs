using Admin.Models.Database;
using Admin.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult user()   //search
        {
            using (projectdatabaseEntities db = new projectdatabaseEntities())
            {
                return View(db.Systemusers.ToList());
            }
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View(new Systemuser());
        }
        [HttpPost]
        public ActionResult AddUser(Systemuser systemuser)
        {
            /* if(ModelState.IsValid)
            {
               var db = new projectdatabaseEntities();
                var u = (from e in db.Systemusers
                         where e.U_username.Equals(systemuser.U_username) &&
                         e.U_password.Equals(systemuser.U_password)
                         select e).FirstOrDefault();

                if (u != null)
                {

                }
            var db = new projectdatabaseEntities();
                
                var u = new Systemuser();
                u.U_password = systemuser.U_password;
                u.U_username = systemuser.U_username;
                u.Usertype = "Customer";
                db.Systemusers.Add(u);
                db.SaveChanges();
                return RedirectToAction("AddUser");

            }
            return View("systemuser");*/
            string fileName = Path.GetFileNameWithoutExtension(systemuser.ImageFile.FileName);
            string extension = Path.GetExtension(systemuser.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            systemuser.U_profileimg = "/UImage/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/UImage/"), fileName);
            systemuser.ImageFile.SaveAs(fileName);

            using (projectdatabaseEntities db = new projectdatabaseEntities())
            {
                db.Systemusers.Add(systemuser);
                db.SaveChanges();

            }



            ModelState.Clear();
            return RedirectToAction("user");
        }

        

        /*[HttpGet]
        public ActionResult Edit(int id)
        {
            projectdatabaseEntities db = new projectdatabaseEntities();
            var systemuser = (from s in db.Systemusers
                            where s.Id == id
                            select s).FirstOrDefault();
            return View(systemuser);
        }
        [HttpPost]
        public ActionResult Edit(Systemuser sub_s)  //id_sub user is sending after edit
        {
            projectdatabaseEntities db = new projectdatabaseEntities();
            var systemuser = (from s in db.Systemusers
                            where s.Id == sub_s.Id
                            select s).FirstOrDefault();
            db.Entry(systemuser).CurrentValues.SetValues(sub_s);
            db.SaveChanges();
            return RedirectToAction("user");
        }*/


        [HttpPost]
        public ActionResult Delete (Systemuser s)
        {
            projectdatabaseEntities db = new projectdatabaseEntities();
            var data = (from Systemuser in db.Systemusers
                        where Systemuser.Id == s.Id
                        select Systemuser).FirstOrDefault();
            db.Systemusers.Remove(data);
            db.SaveChanges();
            return RedirectToAction("user");
        }

        [HttpGet]
        public ActionResult SellerList()
        {
            projectdatabaseEntities db = new projectdatabaseEntities();
            var SellerList = (from s in db.Systemusers
                                where s.Usertype.Equals("3")
                                select s).ToList();
            return View(SellerList);
        }

        [HttpGet]
        public ActionResult DeliveryManList()
        {
            projectdatabaseEntities db = new projectdatabaseEntities();
            var DeliveryList = (from s in db.Systemusers
                              where s.Usertype.Equals("4")
                              select s).ToList();
            return View(DeliveryList);
        }


        [HttpGet]
        public ActionResult CustomerList()
        {
            projectdatabaseEntities db = new projectdatabaseEntities();
            var CustomerList = (from s in db.Systemusers
                                where s.Usertype.Equals("2")
                                select s).ToList();
            return View(CustomerList);
        }
 
        public ActionResult totalSeller()
        {
           /* projectdatabaseEntities db = new projectdatabaseEntities();
            var totalList = (from s in db.Systemusers
                              where s.Usertype.Equals("3")
                              select s).Count();
            return View(totalList);*/

            TotalSeller();
            return View();

             
        }

        private void TotalSeller()
        {
            projectdatabaseEntities db = new projectdatabaseEntities();
            ViewBag.Count = db.Products.Count();
            ViewBag.Countp = db.Systemusers.Count();
        }


        /*public ActionResult totalProduct()
        {
            

            TotalProduct();
            return View();
        }

        private void TotalProduct()
        {
            projectdatabaseEntities db = new projectdatabaseEntities();
            ViewBag.Countp = db.Products.Count();
        }*/
    }
}