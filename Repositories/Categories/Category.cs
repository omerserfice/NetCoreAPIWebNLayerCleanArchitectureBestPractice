﻿using App.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Categories
{
    public class Category : IAuditEntity
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Product>? Products { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Updated { get; set; }
	}
}
