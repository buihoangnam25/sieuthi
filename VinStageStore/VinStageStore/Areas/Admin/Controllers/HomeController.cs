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

            OrderUser orderUser = new OrderUser();
            orderUser.ListOrder = lstOrder;
            orderUser.User = quantityUser;

            return View(orderUser);
        }
    }
}