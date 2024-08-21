using EMSProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EMSProject.Controllers
{
    public class EquipmentController : Controller
    {
        private EMSEntitiesdb db = new EMSEntitiesdb();

        // GET: equip
        public ActionResult Index()
        {
            var equipments = db.equipments.Include(e => e.category);
            
            return View(equipments.ToList());
        }

        // GET: equip/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipment equipment = db.equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // GET: equip/Create
        public ActionResult Create()
        {
            ViewBag.equiptype = new SelectList(db.categories, "id", "name");
            return View();
        }

        // POST: equip/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,price,equiptype,purchdate")] equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.equipments.Add(equipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.equiptype = new SelectList(db.categories, "id", "name", equipment.equiptype);
            return View(equipment);
        }

        // GET: equip/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipment equipment = db.equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.equiptype = new SelectList(db.categories, "id", "name", equipment.equiptype);
            return View(equipment);
        }

        // POST: equip/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,price,equiptype,purchdate")] equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.equiptype = new SelectList(db.categories, "id", "name", equipment.equiptype);
            return View(equipment);
        }

        // GET: equip/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipment equipment = db.equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: equip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            equipment equipment = db.equipments.Find(id);
            db.equipments.Remove(equipment);
            db.SaveChanges();
            return RedirectToAction("Index");
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
