using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductList.Models;

namespace ProductList.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            List<product> productList = new List<product>();   //class
            for(int i=0;i<10;i++)
            {
                var p = new product()
                {
                    Id = i + 1,
                    Name = "Product " + (i + 1),
                    Details = "This is a Demo product for people who wants best servies at low cost.",
                    Price = 100 * i,

                };

                productList.Add(p);
            }
            return View(productList);
        }
    }
}