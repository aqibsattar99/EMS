using EMSProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMSProject.Controllers
{
    public class DashboardController : Controller
    {
        private EMSEntitiesdb db = new EMSEntitiesdb();

        // GET: equip
        public ActionResult Index()
        {
            var totalequipments = db.equipments.ToList().Count();
            var equiplist = db.equipments.ToList();
            var totalCategories = db.categories.Count();
            // Calculate the sum of the price field
            var totalPriceSum = db.equipments.Sum(p => p.price);

            // Store the total price sum in ViewBag
            ViewBag.TotalPriceSum = totalPriceSum;
            ViewBag.TotalEquipments = totalequipments;
            ViewBag.TotalCategories = totalCategories;

            return View(equiplist);
        }

    }
}