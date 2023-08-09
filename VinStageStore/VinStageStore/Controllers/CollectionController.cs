using System;
using System.Collections.Generic;
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

		public ActionResult ProductCategory(int? Id) 
		{
			var objCategpry = new List<Category>();
			var objProduct = new List<Product>();

			if (Id != null)
			{
				objProduct = objModel.Products.Where(n => n.CategoryId == Id).ToList();
			}
			else
			{
				objProduct = objModel.Products.ToList();
			}
			objCategpry = objModel.Categories.ToList();
			ProductCategory productCategory = new ProductCategory();
			productCategory.ListProduct = objProduct;
			productCategory.ListCategory = objCategpry;

			return View(productCategory);
		}
    }
}