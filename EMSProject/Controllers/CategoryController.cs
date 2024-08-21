using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EMSProject.Models;

namespace EMSProject.Controllers
{
    public class CategoryController : Controller
    {
        private EMSEntitiesdb db = new EMSEntitiesdb();

        // GET: Category
        public ActionResult Index()
        {
            return View(db.categories.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }



        // Get List of Categories
        [HttpGet]
        public JsonResult GetCategories()
        {
            var categories = db.categories.Select(c => new
            {
                Id = c.id,
                Name = c.name
            }).ToList();

            return Json(categories, JsonRequestBehavior.AllowGet);
        }



        // Add Record
        [HttpPost]
        public ActionResult Create(category cate)
        {
           
                db.categories.Add(cate);
                db.SaveChanges();
                return Json("Success");
        }


        // Get Single Record
        [HttpPost]
        public ActionResult GetSingle(int Id)
        {
        
                var listcat = db.categories.Where(c => c.id == Id).FirstOrDefault();
                return Json(listcat);
        }




        // Update 
        [HttpPost]
        public ActionResult Update(category cat)
        {

            var existingEntity = db.categories.Where(c => c.id == cat.id).FirstOrDefault();

            existingEntity.name = cat.name;
            db.Entry(existingEntity);
            db.SaveChanges();

            return Json("Success !!!");
        }












        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            category category = db.categories.Find(id);
            db.categories.Remove(category);
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
