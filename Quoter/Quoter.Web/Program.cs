using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using Quoter.Web.Extensions;
using Quoter.Web.Models.Configuration;
using Quoter.Web.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Quoter.Web.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

// Setup the database
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
	options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
	{
		options.SignIn.RequireConfirmedAccount = false;
		options.SignIn.RequireConfirmedEmail = false;
		options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
		options.Lockout.MaxFailedAccessAttempts = 5;
		options.User.RequireUniqueEmail = true;
		options.Password.RequireDigit = false;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequiredLength = 4;
		options.Password.RequiredUniqueChars = 0;
		options.Password.RequireUppercase = false;
		options.Password.RequireLowercase = false;

	})
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddSwaggerGen(c =>
{
	c.OperationFilter<SwaggerRegistrationHeaderParameter>();
});

// Web Application services
builder.Services.Configure<UsersConfiguration>(builder.Configuration.GetSection(UsersConfiguration.JsonKey));
builder.Services.AddScoped<IFileVersionsService, FileVersionsService>();
builder.Services.AddScoped<IAppVersionService, AppVersionService>();

WebApplication app = builder.Build();

// Migrate the database if needed
app.MigrateDatabase();
// Seed data in the database if needed
app.SeedDatabase(builder.Configuration).Wait();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}");
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
	ForwardedHeaders = ForwardedHeaders.XForwardedFor |
	ForwardedHeaders.XForwardedProto
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();


app.Run();
