using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinStageStore.Context;

namespace VinStageStore.Models
{
	public class ProductCategory
	{
		public List<Product> ListProduct { get; set; }

		public List<Category> ListCategory { get; set; }
	}
}