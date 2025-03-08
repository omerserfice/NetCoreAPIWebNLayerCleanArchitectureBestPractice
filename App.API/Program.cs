
using App.Repositories.Extensions;
using App.Services;
using App.Services.Extensions;
using Microsoft.AspNetCore.Mvc;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
	options.Filters.Add<FluentValidationFilter>();
	options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll",
		policy =>
		{
			policy.WithOrigins("https://desktop.postman.com") // Postman'in masaüstü sürümüne izin ver
				  .AllowAnyMethod()
				  .AllowAnyHeader()
				  .AllowCredentials();
		});
});



var app = builder.Build();

app.UseExceptionHandler(x => { });

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
