using StudentCrud.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentCrud.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {     //db retrival
            StudentCrudEntities db = new StudentCrudEntities();
            var data = db.studentCs.ToList();
           // return View(new List<studentC>());
            return View(data);
        }
       /* public ActionResult HighCG()    
        {
            StudentCrudEntities db = new StudentCrudEntities();
            var data=(from s in db.studentCs 
                where s.CGPA>3.00 select s).ToList();

            return View(data);
        }
       */
        [HttpGet]
        public ActionResult Create()   // view created
        {
            return View(new studentC()); //table name
        }
        [HttpPost]
        public ActionResult Create(studentC s)
        {
            if(ModelState.IsValid)
            {//insertion on db
                StudentCrudEntities db = new StudentCrudEntities();
                db.studentCs.Add(s);   //table representative
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
         
            
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            StudentCrudEntities db = new StudentCrudEntities();
            var studentC = (from s in db.studentCs
                          where s.Id == id
                          select s).FirstOrDefault();
            return View(studentC);
        }
        [HttpPost]
        public ActionResult Edit(studentC sub_s)  //id_sub user is sending after edit
        {
            StudentCrudEntities db = new StudentCrudEntities();
            var studentC = (from s in db.studentCs
                            where s.Id == sub_s.Id
                            select s).FirstOrDefault();
            db.Entry(studentC).CurrentValues.SetValues(sub_s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            StudentCrudEntities db = new StudentCrudEntities();
            var studentC = (from s in db.studentCs
                            where s.Id == id
                            select s).FirstOrDefault();
            return View(studentC);
        }
        [HttpPost]
        public ActionResult Delete(studentC sub_s)  //id_sub user is sending after edit
        {
            StudentCrudEntities db = new StudentCrudEntities();
            var studentC = (from s in db.studentCs
                            where s.Id == sub_s.Id
                            select s).FirstOrDefault();
            db.studentCs.Remove(studentC);
           // db.Entry(studentC).CurrentValues.SetValues(sub_s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
      public ActionResult SpecialScholarship()
       {
           StudentCrudEntities db = new StudentCrudEntities();
           var specialScholarship = (from s in db.studentCs
                                     where s.Age >= 25
                                     select s).ToList();
           return View(specialScholarship);

       }

        [HttpGet]
        public ActionResult Scholarship()
        {
            StudentCrudEntities db = new StudentCrudEntities();
            var scholarship = (from s in db.studentCs
                               where s.CGPA >= 3.75
                               select s).ToList();
            return View(scholarship);

        }


    }
}