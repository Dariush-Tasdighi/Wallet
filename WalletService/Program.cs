// **************************************************
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// **************************************************

// **************************************************
var builder =
	Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);
// **************************************************

// **************************************************
// AddHttpContextAccessor() -> using Microsoft.Extensions.DependencyInjection;
builder.Services.AddHttpContextAccessor();
// **************************************************

// **************************************************
builder.Services.AddRouting(options =>
{
	options.LowercaseUrls = true;
	options.LowercaseQueryStrings = true;

	//options.AppendTrailingSlash
	//options.SuppressCheckForUnhandledSecurityMetadata = false;
});
// **************************************************

// **************************************************
builder.Services.AddScoped<Infrastructure.IUtility, Infrastructure.Utility>();
// **************************************************

// **************************************************
builder.Services.AddControllers();
// **************************************************

// **************************************************
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
	options.CustomSchemaIds(current => current.ToString());
});
// **************************************************

// **************************************************
// GetConnectionString() -> using Microsoft.Extensions.Configuration;
var connectionString =
	builder.Configuration.GetConnectionString(name: "ConnectionString");

// AddDbContext -> using Microsoft.Extensions.DependencyInjection;
builder.Services.AddDbContext<Data.DatabaseContext>
	(optionsAction: options =>
	{
		// از دستور ذیل استفاده نمی‌کنیم
		//options
		//	// using Microsoft.EntityFrameworkCore;
		//	.UseLazyLoadingProxies();

		options
			// using Microsoft.EntityFrameworkCore;
			.UseSqlServer(connectionString: connectionString);
	});
// **************************************************

// **************************************************
var app =
	builder.Build();
// **************************************************

// **************************************************
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
// **************************************************

// **************************************************
app.UseHttpsRedirection();
// **************************************************

// **************************************************
app.UseAuthorization();
// **************************************************

// **************************************************
app.MapControllers();
// **************************************************

// **************************************************
app.Run();
// **************************************************
