using App.Repositories.Products;
using App.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Products;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using App.Services.ExceptionHandlers;
using App.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using App.Services.Filters;

namespace App.Services.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<ICategoryService,CategoryService>();
			services.AddScoped(typeof(NotFoundFilter<,>));

			services.AddFluentValidationAutoValidation();
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			services.AddExceptionHandler<CriticalExceptionHandler>();
			services.AddExceptionHandler<GlobalExceptionHandler>();
			return services;
		}


	}
}
