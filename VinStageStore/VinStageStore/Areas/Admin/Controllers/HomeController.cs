using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinStageStore.Context;
using VinStageStore.Models;

namespace VinStageStore.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        VinStageShopEntities db = new VinStageShopEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            var lstOrder = db.Orders.ToList();
            int quantityUser = db.Users.Count();
            var lstProduct = db.Products.Where(n => n.Quantity <= 10).OrderBy(n => n.Quantity).ToList();

            OrderUser orderUser = new OrderUser();
            orderUser.ListOrder = lstOrder;
            orderUser.User = quantityUser;
			orderUser.ListProduct = lstProduct;


			return View(orderUser);
        }
    }
}