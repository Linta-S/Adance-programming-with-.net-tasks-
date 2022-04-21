using Admin.Models.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            using (projectdatabaseEntities db = new projectdatabaseEntities())
            {
                return View(db.Products.ToList());
            }
    }
        
        public ActionResult ProductList()
        {
            using (projectdatabaseEntities db = new projectdatabaseEntities())
            {
                return View(db.Products.ToList());
            }
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View(new Product());

        }
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
             
                string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extension = Path.GetExtension(product.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                product.P_img = "/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                product.ImageFile.SaveAs(fileName);

                using (projectdatabaseEntities db = new projectdatabaseEntities())
                {
                    db.Products.Add(product);
                    db.SaveChanges();

                }



                ModelState.Clear();
                return RedirectToAction("ProductList");
            
            
        }
         


        public ActionResult ReturnProduct()
        {
            return View();
        }

        public ActionResult ProductTable()
        {
            using (projectdatabaseEntities db = new projectdatabaseEntities())
            {
                return View(db.Products.ToList());
            }
        }

        /*[HttpPost]
        public ActionResult ProductDelete(Product um)
        {
            projectdatabaseEntities db = new projectdatabaseEntities();
            var data = (from Rating in db.Ratings
                        where Rating.Product_id == r.Product_id
                        select Rating).FirstOrDefault();
             db.Ratings.Remove(data);

            var dataa = (from product in db.Products
                        where product.Id == p.Id
                        select product).FirstOrDefault();

     

            db.Products.Remove(dataa);
            db.SaveChanges();
            return RedirectToAction("ProductList");
    }*/
        [HttpGet]
        public ActionResult Edit(int id)
        {
            projectdatabaseEntities db = new projectdatabaseEntities();
            var product = (from s in db.Products
                            where s.Id == id
                            select s).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product sub_s)  //id_sub user is sending after edit
        {
            projectdatabaseEntities db = new projectdatabaseEntities();
            var product = (from s in db.Products
                            where s.Id == sub_s.Id
                            select s).FirstOrDefault();
            db.Entry(product).CurrentValues.SetValues(sub_s);
            db.SaveChanges();
            return RedirectToAction("ProductTable");
        }


        /* [HttpGet]
         public ActionResult Delete(int id)
         {
             projectdatabaseEntities db = new projectdatabaseEntities();
             var product = (from s in db.Products
                             where s.Id == id
                             select s).FirstOrDefault();
             return View(product);
         }
         [HttpPost]
         public ActionResult Delete(Product sub_s)  //id_sub user is sending after edit
         {
             projectdatabaseEntities db = new projectdatabaseEntities();
             var product = (from s in db.Products
                             where s.Id == sub_s.Id
                             select s).FirstOrDefault();
             db.Products.Remove(product);
             // db.Entry(studentC).CurrentValues.SetValues(sub_s);
             db.SaveChanges();
             return RedirectToAction("ProductTable");
         }

         */

        [HttpPost]
        public ActionResult Delete(Product p)
        {
            projectdatabaseEntities db = new projectdatabaseEntities();
            var data = (from Product in db.Products
                        where Product.Id == p.Id
                        select Product).FirstOrDefault();

            db.Products.Remove(data);
            db.SaveChanges();
            return RedirectToAction("ProductTable");
        }



    }
}