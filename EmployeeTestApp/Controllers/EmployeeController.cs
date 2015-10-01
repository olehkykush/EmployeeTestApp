using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeTestApp.Models;
using EmployeeTestApp.DAL;

namespace EmployeeTestApp.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeContext db = new EmployeeContext();

        // GET: /Employee/ (View)
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Employee/IndexVM
        public ActionResult IndexVM()
        {
            var model = db.Employees.ToList();
            List<Object> list = new List<Object>();
            foreach (var item in model)
            {
                list.Add(packEmployeeObject(item));
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: /Employee/DetailsVM/3
        public ActionResult DetailsVM(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return Json(packEmployeeObject(employee), JsonRequestBehavior.AllowGet);
        }

        // POST: /Employee/CreateVM
        [HttpPost]
        public ActionResult CreateVM(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                Employee succeedEmployee = db.Employees.Include(i => i.Company).SingleOrDefault(x => x.EmployeeID == employee.EmployeeID);
                return Json(packEmployeeObject(succeedEmployee), JsonRequestBehavior.AllowGet);
            }

            return Json(new { respond = "Error, entry failed" }, JsonRequestBehavior.AllowGet);
        }

        // POST: /Employee/EditVM
        [HttpPost]
        public ActionResult EditVM(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                Employee succeedEmployee = db.Employees.Include(i => i.Company).SingleOrDefault(x => x.EmployeeID == employee.EmployeeID);
                return Json(packEmployeeObject(succeedEmployee), JsonRequestBehavior.AllowGet);
            }
            return Json(new { respond = "Error, entry failed" }, JsonRequestBehavior.AllowGet);
        }

        // POST: /Employee/DeleteVM/3
        [HttpPost]
        public ActionResult DeleteVM(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();

            return Json(new { respond = "Deleted" }, JsonRequestBehavior.AllowGet);
        }

        //========================

        // GET: /Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: /Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,LastName,FirstMidName,CompanyID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: /Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,LastName,FirstMidName,CompanyID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: /Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private Object packEmployeeObject(Employee employee)
        {
            var obj = new
            {
                EmployeeID = employee.EmployeeID,
                LastName = employee.LastName,
                FirstMidName = employee.FirstMidName,
                CompanyID = employee.CompanyID,
                Company = new
                {
                    CompanyID = employee.Company.CompanyID,
                    CompanyName = employee.Company.CompanyName
                }
            };
            return obj;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
