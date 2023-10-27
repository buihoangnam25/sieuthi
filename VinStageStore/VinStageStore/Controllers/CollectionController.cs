using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinStageStore.Context;
using VinStageStore.Models;

namespace VinStageStore.Controllers
{
	public class CollectionController : Controller
	{
		VinStageShopEntities objModel = new VinStageShopEntities();
		// GET: Collection

		public ActionResult ProductCategory(int? Id, int? page)
		{
			const int pageSize = 6;
			var objProduct = (Id != null)
				? objModel.Products.Where(n => n.CategoryId == Id).ToList()
				: objModel.Products.ToList();
			var objCategpry = objModel.Categories.ToList();


			int pageNumber = page ?? 1;
			ProductCategory productCategory = new ProductCategory();
			productCategory.ListProduct = objProduct.Skip((pageNumber - 1) * pageSize).Take(12).ToList();
			productCategory.ListCategory = objCategpry;
			productCategory.pageSize = pageSize;
			ViewBag.CurrentPage = pageNumber;

			return View(productCategory);
		}
	}
}