﻿using OdeToFood.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class RestaurantController : Controller
    {
        private IOdeToFoodDb _db;

        public RestaurantController()
        {
            _db = new OdeToFoodDb();
        }

        public RestaurantController(IOdeToFoodDb db)
        {
            _db = db;
        }

        //
        // GET: /Restaurant/

        public ActionResult Index()
        {
            return View(_db.Query<Restaurant>().ToList());
        }

        //
        // GET: /Restaurant/Create

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Restaurant/Create

        [HttpPost]
        [Authorize(Roles="Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _db.Add(restaurant);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        //
        // GET: /Restaurant/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Restaurant restaurant = _db.Query<Restaurant>().Single(r => r.Id == id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        //
        // POST: /Restaurant/Edit/5

        [HttpPost]
        public ActionResult Edit(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _db.Update(restaurant);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        //
        // GET: /Restaurant/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Restaurant restaurant = _db.Query<Restaurant>().Single(r => r.Id == id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        //
        // POST: /Restaurant/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = _db.Query<Restaurant>().Single(r => r.Id == id);
            _db.Remove(restaurant);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
