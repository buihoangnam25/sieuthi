using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinStageStore.Context;

namespace VinStageStore.Models
{
	public class OrderUser
	{
		public int User { get; set; }

		public List<Order> ListOrder { get; set; }

		public List<Product> ListProduct { get; set; }
	}
}