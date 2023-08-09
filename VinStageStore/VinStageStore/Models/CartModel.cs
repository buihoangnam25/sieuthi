using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinStageStore.Context;

namespace VinStageStore.Models
{
	public class CartModel
	{

		//lấy thông tin sản phẩm
		public Product Product { get; set; }

		//lấy số lượng thêm vào cart
		public int Quantity { get; set; }

	}
}