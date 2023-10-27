using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VinStageStore.Context;
using VinStageStore.Models;

namespace VinStageStore.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private VinStageShopEntities db = new VinStageShopEntities();

        // GET: Admin/Orders
        public ActionResult Index(string sortBy)
        {
            var orders = db.Orders.Include(o => o.User).OrderBy(o => o.Status);

			switch (sortBy)
			{
				case "TotalPriceAsc":
					orders = orders.OrderBy(o => o.TotalPrice);
					break;
				case "TotalPriceDesc":
					orders = orders.OrderByDescending(o => o.TotalPrice);
					break;
				case "OrderDateAsc":
					orders = orders.OrderBy(o => o.OrderDate);
					break;
				case "OrderDateDesc":
					orders = orders.OrderByDescending(o => o.OrderDate);
					break;
				default:
					break;
			}

			return View(orders.ToList());
        }

		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var order = db.Orders.Find(id);
            List<OrderItem> OrderItems = db.OrderItems.Where(n => n.OrderId == order.Id).ToList();
            
            OrderItemOrder item =  new OrderItemOrder();
            item.Order = order;
            item.ListOrderItem = OrderItems;


			return View(item);
		}
		// GET: Admin/Orders/Edit/5
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", order.UserId);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,OrderDate,Name,TotalPrice,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                //Bỏ gắn userId để khi save vẫn lấy giữ nguyên giá trị của userID
				var existingOrder = db.Orders.Find(order.Id);
				existingOrder.OrderDate = order.OrderDate;
				existingOrder.Name = order.Name;
				existingOrder.TotalPrice = order.TotalPrice;
				existingOrder.Status = order.Status;
				db.Entry(existingOrder).State = EntityState.Modified;

				var orderEdit = db.Orders.Find(order.Id);
				List<OrderItem> OrderItems = db.OrderItems.Where(n => n.OrderId == orderEdit.Id).ToList();

				//xử lý : khi order chuyển sang trạng thái thành công thì quantity bên product cũng phải giảm
				if (order.Status == 3)
                {
					UpdateProductQuantities(OrderItems , order);
				}

				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", order.UserId);
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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

		private void UpdateProductQuantities(List<OrderItem> OrderItems , Order order)
		{

			foreach (var orderItem in OrderItems)
			{
				if (orderItem.Product != null && orderItem.Quantity > 0)
				{
					// Kiểm tra nếu trạng thái của đơn hàng đã chuyển thành 3 (thành công)
					if (order.Status == 3)
					{
						// Giảm số lượng sản phẩm bên Products dựa trên OrderItem
						var product = db.Products.Find(orderItem.ProductId);
						if (product != null)
						{
							product.Quantity -= orderItem.Quantity;
						}
					}
				}
			}
			db.SaveChanges();
		}
	}
}
