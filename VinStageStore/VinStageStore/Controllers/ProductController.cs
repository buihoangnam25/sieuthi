using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinStageStore.Context;
using VinStageStore.Models;

namespace VinStageStore.Controllers
{
    public class ProductController : Controller
    {
		VinStageShopEntities objModel = new VinStageShopEntities();
		// GET: Product
		public ActionResult Detail(int? Id)
        {
			var objProductDetail = objModel.Products.Where(n => n.Id == Id).FirstOrDefault();


			//lấy các sản phẩm liên quan 
			var lstProduct = objModel.Products.Where(x => x.CategoryId == objProductDetail.CategoryId).ToList();
			Products objProducts = new Products();
			objProducts.objProductDetail = objProductDetail;
			objProducts.lstRelatedProducts = lstProduct;
			return View(objProducts);
        }
    }
}