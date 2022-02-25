using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    public class UsersController : Controller
    {
        private DBContext db = new DBContext();

        public ActionResult index()
        {
            return View();
        }

        //user login :POST
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult index(User user)
        {
           
            var users = db.users.Where(a => a.empId.Equals(user.empId) && a.password.Equals(user.password)).FirstOrDefault();
            if (users != null)
            {
                FormsAuthentication.SetAuthCookie(users.emailId, false);
                Session["EmpId"] = users.empId.ToString();
                Session["MailId"] = users.emailId.ToString();
                Session["Username"] = users.name.ToString();
                Session["TowerLead"] = users.isTowerLead.ToString();
                return RedirectToAction("Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login credentials");
                return View();
            }
        }
        [Authorize]
        public ActionResult Home()
        {
            if (User.Identity.Name != null)
            {
                return View();
            }
            return RedirectToRoute("default");
        }
        //User Logout action
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToRoute("default");
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
