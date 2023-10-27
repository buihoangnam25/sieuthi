using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VinStageStore.Context;
using VinStageStore.Models;

namespace VinStageStore.Controllers
{
    public class OrderController : Controller
    {
		private VinStageShopEntities db = new VinStageShopEntities();
		// GET: Order
		public ActionResult Index()
        {
			int idUser = 0;
			idUser = (int)Session["idUser"];

			List<Order> orders = db.Orders.Where(n => n.UserId == idUser).ToList();
			List<OrderItem> orderItems = new List<OrderItem>();
			OrderItemCustomer orderItemCustomer = new OrderItemCustomer();
			orderItemCustomer.ListOrder = orders;

			orders = orders.Where(n => n.Status == 3).ToList();
			foreach (var order in orders)
			{
				List<OrderItem> itemsForOrder = db.OrderItems.Where(oi => oi.OrderId == order.Id).ToList();
				orderItems.AddRange(itemsForOrder);
			}
			orderItemCustomer.ListOrderItem = orderItems;

			return View(orderItemCustomer);

			//int iduser = 0;
			//iduser = (int)Session["iduser"];
			//List<Order> order = db.Orders.Where(n => n.UserId == idUser).ToList();
			//var orderItem =  db.OrderItems.ToList();

			//return View(order);
		}

		public ActionResult Edit(int? Id)
		{
			int deleteOrder = 4;
			var order = db.Orders.Where( n => n.Id == Id).FirstOrDefault();

			order.Status = deleteOrder;
			db.SaveChanges();

			return RedirectToAction("Index");
		}



	}
}