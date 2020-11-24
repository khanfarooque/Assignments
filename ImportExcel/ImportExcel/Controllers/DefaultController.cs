using ImportExcel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImportExcel.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            using (ImportExcelDBEntities db = new ImportExcelDBEntities())
            {
                List<Employee> employees = db.Employees.ToList<Employee>();
                
                return View(employees.ToList());

            }
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                ImportExcelDBEntities db = new ImportExcelDBEntities();
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public ActionResult Edit(int emp_id)
        {
            
            ImportExcelDBEntities db = new ImportExcelDBEntities();
            Employee employee = db.Employees.Find(emp_id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                ImportExcelDBEntities db = new ImportExcelDBEntities();
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public ActionResult Delete(int emp_id)
        {
            ImportExcelDBEntities db = new ImportExcelDBEntities();
            Employee employee = db.Employees.Find(emp_id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int emp_id)
        {
            ImportExcelDBEntities db = new ImportExcelDBEntities();
            Employee employee = db.Employees.Find(emp_id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetData()
        {
            using(ImportExcelDBEntities db=new ImportExcelDBEntities())
            {
                List<Employee> employees = db.Employees.ToList<Employee>();
                //return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
                return Json(employees, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ImportData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportData(List<Employee> employees)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (ImportExcelDBEntities db = new ImportExcelDBEntities())
                {
                    foreach (var i in employees)
                    {
                        db.Employees.Add(i);
                    }
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        //public ActionResult SaveData(List<Employee> employees)
        //{
        //    bool status = false;
        //    if (ModelState.IsValid)
        //    {
        //        using(ImportExcelDBEntities db=new ImportExcelDBEntities())
        //        {
        //            foreach(var i in employees)
        //            {
        //                db.Employees.Add(i);
        //            }
        //            db.SaveChanges();
        //            status = true;
        //        }
        //    }
        //    return new JsonResult { Data = new { status = status } };
        //}
    }
}