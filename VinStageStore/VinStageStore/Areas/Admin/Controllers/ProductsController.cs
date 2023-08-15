using PagedList;
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
	public class ProductsController : Controller
	{
		private VinStageShopEntities db = new VinStageShopEntities();

		// GET: Admin/Products
		public ActionResult Index(string curentFilter ,string SearchString , int? page)
		{
			var lstProduct = new List<Product>();
			if(SearchString != null)
			{
				page = 1;
			}
			else
			{
				SearchString = curentFilter;
			}
			if (!string.IsNullOrEmpty(SearchString))
			{
				//lấy sản phẩm theo khóa SearchString
				lstProduct = db.Products.Where(n => n.Name.Contains(SearchString)).ToList();
			}
			else
			{
				lstProduct = db.Products.ToList();
			}
			ViewBag.CurrentFilter = SearchString;


			int pageSize = 6; // Số sản phẩm trên mỗi trang
			int pageNumber = (page ?? 1); // Trang hiện tại (mặc định là 1)

			//sắp xếp sản phẩm theo id , những id mới tạo sẽ lên trước
			lstProduct =lstProduct.OrderByDescending(n => n.Id).ToList();


			return View(lstProduct.ToPagedList(pageNumber , pageSize));
		}

		// GET: Admin/Products/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// GET: Admin/Products/Create
		public ActionResult Create()
		{
			ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
			return View();
		}

		// POST: Admin/Products/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Product product)
		{
			try
			{
				if (product.ImageUpload != null)
				{
					string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
					string extension = Path.GetExtension(product.ImageUpload.FileName);
					fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhss")) + extension;
					product.ImageUrl = fileName;
					product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));

				}

				if (product.ImageDetailUpload != null)
				{
					string fileName = Path.GetFileNameWithoutExtension(product.ImageDetailUpload.FileName);
					string extension = Path.GetExtension(product.ImageDetailUpload.FileName);
					fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhss")) + extension;
					product.ImageDetail = fileName;
					product.ImageDetailUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));

				}
				db.Products.Add(product);
				db.SaveChanges();
				return RedirectToAction("Index");

			}
			catch (Exception)
			{
				return RedirectToAction("Index");
			}


			ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
			return View(product);
		}

		// GET: Admin/Products/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
			return View(product);
		}

		// POST: Admin/Products/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Product product)
		{
			try
			{
				if (product.ImageUpload != null)
				{
					string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
					string extension = Path.GetExtension(product.ImageUpload.FileName);
					fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhss")) + extension;
					product.ImageUrl = fileName;
					product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));

				}

				if (product.ImageDetailUpload != null)
				{
					string fileName = Path.GetFileNameWithoutExtension(product.ImageDetailUpload.FileName);
					string extension = Path.GetExtension(product.ImageDetailUpload.FileName);
					fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhss")) + extension;
					product.ImageDetail = fileName;
					product.ImageDetailUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));

				}

				db.Entry(product).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			catch (Exception)
			{
				return RedirectToAction("Index");
			}


			ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
			return View(product);
		}

		// GET: Admin/Products/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// POST: Admin/Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Product product = db.Products.Find(id);
			db.Products.Remove(product);
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
