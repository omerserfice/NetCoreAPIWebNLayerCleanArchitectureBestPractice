﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Categories
{
	public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
	{
	

		public Task<Category?> GetCategoryWithProductAsync(int id)
		{
		return	context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
		}
		public IQueryable<Category> GetCategoryByProductsAsync(int id)
		{
			return context.Categories.Include(x => x.Products).AsQueryable();
		}

	}
}
