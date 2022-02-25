using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    public class RostersController : Controller
    {
        private DBContext db = new DBContext();


        // GET: Rosters
        public ActionResult Index()
        {
            var rosters = db.rosters.Include(r => r.userassigned).OrderBy(date=>date.startDate);

                return View(rosters.ToList());
        }

        // GET: Rosters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roster roster = db.rosters.Find(id);
            if (roster == null)
            {
                return HttpNotFound();
            }
            return View(roster);
        }

        // GET: Rosters/Create
        public ActionResult Create()
        {

            ViewBag.userAssignedId = new SelectList(db.userAgs, "userassignedId", "userassignedId");
            ViewBag.empId = new SelectList(db.users, "empId", "emailId");
            ViewBag.agId = new SelectList(db.assignmentGroups, "agId", "agName");

            return View();
        }

        // POST: Rosters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string empId,int agId,[Bind(Include = "rosterId,userAssignedId,isOneDay,startDate,endDate,onCall,shift,empId,agId")] Roster roster)
        {
            ModelState.Remove("UserAssignedId");
            if (ModelState.IsValid)
            {
                if (roster.isOneDay)
                {
                    roster.endDate = roster.startDate;
                }
               // roster.userAssignedId = Convert.ToInt32(db.userAgs.Where(x => x.agId.Equals(agId) && x.empId.Equals(empId)).ToList());
                roster.userAssignedId = db.userAgs.Where(x => x.agId.Equals(agId) && x.empId.Equals(empId)).Select(x => x.userassignedId).FirstOrDefault();
                db.rosters.Add(roster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.empId = new SelectList(db.users, "empId", "emailId");
            ViewBag.agId = new SelectList(db.assignmentGroups, "agId", "agName");
            ViewBag.userAssignedId = new SelectList(db.userAgs, "userassignedId", "userassignedId", roster.userAssignedId);
            
            return View(roster);
        }

        // POST: Rosters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rosterId,userAssignedId,isOneDay,startDate,endDate,onCall,shift")] Roster roster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userAssignedId = new SelectList(db.userAgs, "userassignedId", "empId", roster.userAssignedId);
            return View(roster);
        }

        // GET: Rosters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roster roster = db.rosters.Find(id);
            if (roster == null)
            {
                return HttpNotFound();
            }
            return View(roster);
        }

        // POST: Rosters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Roster roster = db.rosters.Find(id);
            db.rosters.Remove(roster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search(string search,string SearchItem,string searchDate)
        {
            var model = new List<Roster>();
            if(search!=null && search != "")
            {
                if (SearchItem == "1") 
                { 
                  model = db.rosters.Where(x => x.userassigned.empId == search).ToList();
                }
                else if (SearchItem == "2")
                {
                    model = db.rosters.Where(x => x.userassigned.user.emailId == search).ToList();
                }
                else if (SearchItem == "3")
                {
                    model = db.rosters.Where(x => x.userassigned.assignmentgroup.team.teamName == search).ToList();
                }
                else if (SearchItem == "4")
                {
                    model = db.rosters.Where(x => x.userassigned.user.name == search).ToList();
                }
                else if (SearchItem == "5")
                {
                    model = db.rosters.Where(x => x.shift == search).ToList();
                }
            }
            if (searchDate != null && searchDate!="" )
            {
                DateTime date = DateTime.Parse(searchDate);
                searchDate = "";
                model = db.rosters.Where(roster => roster.startDate <= date && roster.endDate >= date).OrderBy(dt => dt.startDate).ToList();

            }
            return View(model);
            
        }
        //public ActionResult Search(int? search)
        //{
        //    var model = db.rosters.Where(x => x.startDate == search).ToList();
        //    return View(model);
        //public ActionResult Search(string search)
        //{
        //    var model1 = db.rosters.Where(x => x.userassigned.assignmentgroup.team.teamName == search).ToList();
        //    var model = db.rosters.Where(x => x.userassigned.empId == search).ToList();

        //    return View(model);
        //}
        public ActionResult Searchbyoncall(string oncall, string teamname)
        {
            var model1 = db.rosters.Where(x => x.userassigned.assignmentgroup.team.teamName == teamname && x.onCall == oncall).ToList();

            return View(model1);
        }

        //public ActionResult Searchbyoncall(string oncall)
        //{
        //    var model1 = db.rosters.Where(x=>x.onCall == oncall).ToList();

        //    return View(model1);
        //}

        //public ActionResult Searchbyoncall(string oncall)
        //{
        //    var model1 = db.rosters.Where(x=>x.onCall == oncall).ToList();

        //    return View(model1);
        //}

        //}

        
        [HttpPost]
        public FileResult ExportToExcel()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("empId"),
                                                     new DataColumn("name"),
                                                     new DataColumn("emailId"),
                                                     new DataColumn("AGName"),
                                                     new DataColumn("startDate"),
                                                     new DataColumn("endDate"),
                                                     new DataColumn("onCall"),
                                                     new DataColumn("Shift") });

            var data = db.rosters.OrderBy(date => date.startDate).ToList();

            foreach (var x in data)
            {
                dt.Rows.Add(x.userassigned.user.empId, x.userassigned.user.name, x.userassigned.user.emailId, x.userassigned.assignmentgroup.agName,
                    x.startDate, x.endDate, x.onCall, x.shift);
                    
            }

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream()) //using System.IO;  
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFile.xlsx");
                }
            }
        }
        


    }
}
