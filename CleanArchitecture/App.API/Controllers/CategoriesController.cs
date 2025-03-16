
using App.Application.Features.Categories;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using CleanApp.API.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CleanApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService,IMemoryCache memoryCache) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories() => CreateActionResult(await categoryService.GetAllListAsync());

        [HttpGet("{id}")]
		public async Task<IActionResult> GetCategory(int id)
		{


			return CreateActionResult(await categoryService.GetByIdAsync(id)); 
		}
	

		[HttpGet("products")]
		public async Task<IActionResult> GetCategoryWithProducts() =>
			CreateActionResult(await categoryService.GetCategoryWithProductsAsync());

		[HttpGet("{id}/products")]
		public async Task<IActionResult> GetCategoryWithProducts(int id) => 
            CreateActionResult(await categoryService.GetCategoryWithProductsAsync(id));

        [HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryRequest request) =>
			CreateActionResult(await categoryService.CreateAsync(request));
		[ServiceFilter(typeof(NotFoundFilter<Category, int>))]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest request) =>
            CreateActionResult(await categoryService.UpdateAsync(id, request));

		[ServiceFilter(typeof(NotFoundFilter<Category, int>))]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id) => 
            CreateActionResult(await categoryService.DeleteAsync(id));
	}
}
