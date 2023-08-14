using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinStageStore.Context;
using VinStageStore.Models;

namespace VinStageStore.Controllers
{
    public class PaymentController : Controller
    {
        VinStageShopEntities db = new VinStageShopEntities();
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login" , "Home");
            }
            else
            {
                //lấy thông tin giỏ hàng từ sesion
                var lstCart = (List<CartModel>)Session["cart"];

                //gắn dữ liệu vào tb order
                Order objOrder = new Order();
                objOrder.Name = "Đơn hàng -" + objOrder .Id+ DateTime.Now.ToString("yyyyMMddHHss");
                objOrder.Name = "Đơn hàng -" + DateTime.Now.ToString("yyyyMMddHHss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.OrderDate = DateTime.Now;
                objOrder.Status = 1;
                db.Orders.Add(objOrder);
                //luu thong tin
				db.SaveChanges();


                //lay orderid moi tao de luu vao tb OrderItem
                int orderId = objOrder.Id;

                List<OrderItem> lstOrderItem = new List<OrderItem>();
                foreach (var item in lstCart) 
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.Quantity = item.Quantity;
                    orderItem.OrderId = orderId;
                    orderItem.ProductId = item.Product.Id;
					lstOrderItem.Add(orderItem);
				}
                db.OrderItems.AddRange(lstOrderItem);
                db.SaveChanges();

			}
            return View();
        }
    }
}