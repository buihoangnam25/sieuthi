﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VinStageStore.Context
{
    using System;
    using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations.Schema;

	public partial class Supplier
	{
		public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }

		// tạo biến để có thể chọn file
		[NotMapped]
		public System.Web.HttpPostedFileBase ImageUpload { get; set; }

	}
}
