using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VinStageStore.Context;

namespace VinStageStore.Controllers
{
    public class ReviewsController : Controller
    {
        private VinStageShopEntities db = new VinStageShopEntities();


        // GET: Reviews/Create
        public ActionResult Create(int productId)
        {
			ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
			ViewBag.ProductIdFromParam = productId;
			return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ProductId,Rating,Comment")] Review review)
        {
            if (ModelState.IsValid)
            {
				// Lấy giá trị UserId từ Session
				int userId = int.Parse((string)Session["IdUser"]);
				if (userId > 0)
				{
					review.UserId = userId;
					review.ProductId = ViewBag.ProductIdFromParam;
					db.Reviews.Add(review);
					db.SaveChanges();
					return RedirectToAction("Index");
				}
				else
				{
					// Xử lý lỗi khi không tìm thấy UserId trong Session
					ModelState.AddModelError("", "Lỗi.");
				}
			}

			return View(review);
		}

	}
}
