using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinStageStore.Context;

namespace VinStageStore.Models
{
	public class Products
	{
		public Product objProductDetail { get; set; }
		public List<Product> lstRelatedProducts { get; set; }
	}
}