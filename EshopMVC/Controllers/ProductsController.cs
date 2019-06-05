using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EshopMVC.DAL;
using EshopMVC.Models;

namespace EshopMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly EshopDbContext _db = new EshopDbContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = _db.Products.Include(p => p.Category).Include(p => p.Company);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.CompanyId = new SelectList(_db.Companies, "Id", "PersianName");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,CompanyId,ModelName,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.CompanyId = new SelectList(_db.Companies, "Id", "PersianName", product.CompanyId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.CompanyId = new SelectList(_db.Companies, "Id", "PersianName", product.CompanyId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,CompanyId,ModelName,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(product).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.CompanyId = new SelectList(_db.Companies, "Id", "PersianName", product.CompanyId);
            return View(product);
        }
     

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool Delete(int id)
        {
            Product product = _db.Products.Find(id);
            _db.Products.Remove(product ?? throw new InvalidOperationException());
            _db.SaveChanges();
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
