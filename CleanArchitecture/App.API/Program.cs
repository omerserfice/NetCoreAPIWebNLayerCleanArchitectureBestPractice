using App.Application.Contracts.Caching;
using App.Application.Extensions;
using App.Caching;
using App.Persistance.Extensions;
using CleanApp.API.ExceptionHandler;
using CleanApp.API.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers(options =>
{
	options.Filters.Add<FluentValidationFilter>();
	options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);
builder.Services.AddScoped(typeof(NotFoundFilter<,>));
builder.Services.AddExceptionHandler<CriticalExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheService, CacheService>();	

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseExceptionHandler(x => { });
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
