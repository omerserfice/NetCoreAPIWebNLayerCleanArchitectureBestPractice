﻿using App.Repositories.Products;
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

namespace App.Services.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IProductService, ProductService>();
			services.AddFluentValidationAutoValidation();
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			return services;
		}


	}
}
