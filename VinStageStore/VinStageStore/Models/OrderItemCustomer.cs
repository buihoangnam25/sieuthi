using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinStageStore.Context;

namespace VinStageStore.Models
{
	public class OrderItemCustomer
	{
		public List<Order> ListOrder { get; set; }

		public List<OrderItem> ListOrderItem { get; set; }
	}
}