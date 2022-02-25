using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    public class MomsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Moms
        public ActionResult Index()
        {
            if (User.Identity.Name != null)
            {
                var emailid = User.Identity.Name.ToString();
                Session["mailid"] = emailid;
                var moms = db.moms.Where(mom => mom.createdBy.Equals(emailid)).ToList();
                return View(moms);
            }
            return RedirectToRoute("default");
        }

        // GET: Moms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mom mom = db.moms.Find(id);
            if (mom == null)
            {
                return HttpNotFound();
            }
            return View(mom);
        }

        // GET: Moms/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Moms/New
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New([Bind(Include = "momTitle,momDescription,createdBy")] Mom mom)
        {
            ModelState.Remove("createdOn");
            if (ModelState.IsValid)
            {
                mom.createdBy = User.Identity.Name.ToString();
                mom.createdOn = DateTime.Now;
                db.moms.Add(mom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mom);
        }
        // GET: Todoes/addTask
        public ActionResult addTask(int? id)
        {
            ViewBag.empId = db.users.ToList();
            if (id != null)
            {
                var mom = db.moms.Find(id);
                MomTodo viewModel = new MomTodo{ minutes = mom };
                return View(viewModel);
            }
            return View();
        }

        // POST: Todoes/addTask
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addTask([Bind(Include = "momId,taskDescription,empId,dueDate")] Todo TodoList)
        {
            ModelState.Remove("isActive");
            if (ModelState.IsValid)
            {
                TodoList.isActive = true;
                db.todos.Add(TodoList);
                db.SaveChanges();
                return Redirect("/moms/addTask/"+TodoList.momId);
            }
            MomTodo combinedobject = new MomTodo();
            combinedobject.minutes = db.moms.Find(TodoList.momId);
            ViewBag.empId = db.users.ToList();
            return View(combinedobject);
        }
        // GET: Moms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mom mom = db.moms.Find(id);
            if (mom == null)
            {
                return HttpNotFound();
            }
            return View(mom);
        }

        // POST: Moms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "momId,momName,momDate,momEvent")] Mom mom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mom);
        }

        // GET: Moms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mom mom = db.moms.Find(id);
            if (mom == null)
            {
                return HttpNotFound();
            }
            return View(mom);
        }

        // POST: Moms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mom mom = db.moms.Find(id);
            db.moms.Remove(mom);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Todoes/addTask
        public ActionResult View(int? id)
        {
            ViewBag.empId = db.users.ToList();
            if (id != null)
            {
                var mom = db.moms.Find(id);
                MomTodo viewModel = new MomTodo { minutes = mom };
                return View(viewModel);
            }
            return View();
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
