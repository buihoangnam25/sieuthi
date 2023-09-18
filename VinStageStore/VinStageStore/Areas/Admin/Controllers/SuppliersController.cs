using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VinStageStore.Context;

namespace VinStageStore.Areas.Admin.Controllers
{
    public class SuppliersController : Controller
    {
        private VinStageShopEntities db = new VinStageShopEntities();

        // GET: Admin/Suppliers
        public ActionResult Index()
        {
            return View(db.Suppliers.ToList());
        }

        // GET: Admin/Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Admin/Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            try
            {
               if(supplier.ImageUpload != null)
                {
					string fileName = Path.GetFileNameWithoutExtension(supplier.ImageUpload.FileName);
					string extension = Path.GetExtension(supplier.ImageUpload.FileName);
					fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhss")) + extension;
					supplier.Image = fileName;
					supplier.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
				}

				db.Suppliers.Add(supplier);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			catch (Exception) 
            { 
                return RedirectToAction("Index"); 
            }
        }

        // GET: Admin/Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            try
            {
				if (supplier.ImageUpload != null)
				{
					string fileName = Path.GetFileNameWithoutExtension(supplier.ImageUpload.FileName);
					string extension = Path.GetExtension(supplier.ImageUpload.FileName);
					fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhss")) + extension;
					supplier.Image = fileName;
					supplier.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
				}
				db.Entry(supplier).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
            catch (Exception ) 
            {
				return RedirectToAction("Index");
			}
        }

        // GET: Admin/Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
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
