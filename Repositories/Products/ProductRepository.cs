using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Products
{
	public class ProductRepository(AppDbContext context) : GenericRepository<Product,int>(context), IProductRepository
	{
		public Task<List<Product>> GetTopPriceProductAsync(int count)
		{
			return Context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
		}
	}
}
