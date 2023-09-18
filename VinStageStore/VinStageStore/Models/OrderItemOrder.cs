using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinStageStore.Context;

namespace VinStageStore.Models
{
	public class OrderItemOrder
	{
		//class lay danh don dang va danh sach chi tiet don hang
		public Order Order { get; set; }

		public List<OrderItem> ListOrderItem { get; set; }
	}
}