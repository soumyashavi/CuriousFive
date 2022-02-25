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
    public class TodoesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Todoes
        public ActionResult Index()
        {
            string empid = Session["EmpId"].ToString();
            var todos = db.todos.Where(t => t.empId.Equals(empid)).ToList();

         //   var todos = db.todos.Include(t => t.mom).Include(t => t.user);
            return View(todos.ToList());
        }

        // GET: Todoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.todos.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        

        // GET: Todoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.todos.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            ViewBag.momId = new SelectList(db.moms, "momId", "momName", todo.momId);
            ViewBag.empId = new SelectList(db.users, "empId", "emailId", todo.empId);
            return View(todo);
        }

        // POST: Todoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "todoId,momId,taskDescription,empId,dueDate,isActive")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.momId = new SelectList(db.moms, "momId", "momName", todo.momId);
            ViewBag.empId = new SelectList(db.users, "empId", "emailId", todo.empId);
            return View(todo);
        }

      

        // GET: Todoes/Delete/5
        public ActionResult Delete(int?id)
        {
            Todo todo = db.todos.Find(id);
            var mom = db.todos.Where(t => t.todoId == id).FirstOrDefault();
            int momid = mom.momId;
            db.todos.Remove(todo);
            db.SaveChanges();
            return Redirect("/moms/addtask/"+momid);
        }

        // GET: Complete/
        public ActionResult Complete(int? id)
        {
            Todo todo = db.todos.Find(id);
            todo.isActive = false;
            db.Entry(todo).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("index");
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
