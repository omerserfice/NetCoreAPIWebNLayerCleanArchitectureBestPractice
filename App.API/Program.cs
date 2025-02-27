
using App.Repositories.Extensions;
using App.Services.Extensions;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
