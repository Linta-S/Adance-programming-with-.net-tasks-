using EcommerceWeb.Models.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceWeb.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        public ActionResult Index()
        {
            using (EcommerceEntities db = new EcommerceEntities())
            {
                return View(db.ProductLs.ToList());
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductL());
        }

        [HttpPost]
        public ActionResult Create(ProductL product)
        {
            string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            string extension = Path.GetExtension(product.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            product.Image = "../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../Image/"), fileName);
            product.ImageFile.SaveAs(fileName);
            using (EcommerceEntities db = new EcommerceEntities())
            {
                db.ProductLs.Add(product);
                db.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(ProductL p)
        {
            EcommerceEntities db = new EcommerceEntities();
            var data = (from product in db.ProductLs
                        where product.Id == p.Id
                        select product).FirstOrDefault();

            db.ProductLs.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            EcommerceEntities db = new EcommerceEntities();
            var ProductL = (from p in db.ProductLs
                            where p.Id == id
                            select p).FirstOrDefault();
            return View(ProductL);
        }
        [HttpPost]
        public ActionResult Edit(ProductL sub_s)  //id_sub user is sending after edit
        {
            EcommerceEntities db = new EcommerceEntities();
            var ProductL = (from p in db.ProductLs
                            where p.Id == sub_s.Id
                            select p).FirstOrDefault();
            db.Entry(ProductL).CurrentValues.SetValues(sub_s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ProductList()
        {
            using (EcommerceEntities db = new EcommerceEntities())
            {
                return View(db.ProductLs.ToList());
            }
        }

    }
}