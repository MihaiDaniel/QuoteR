using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.App.Helpers;
using Quoter.Web.Framework.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// AddDbContextPool uses a pool to reuse DbContexts rather then discarding them.
builder.Services.AddDbContextPool<QuoterContext>(options =>
			options.UseNpgsql(builder.Configuration.GetConnectionString("QuoterContext")));

// Add an auth scheme. Multiple schemes can be used, and we can use Authorize attribute
// on pages to specify which auth scheme we want to use
// Here we specify the default scheme (from the ones we have added) to use
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		// Specify custom paths for login, logout, access denied
		//options.LoginPath = "/";
		//options.LogoutPath = "/";
		//options.AccessDeniedPath = "/";
	}); 

var app = builder.Build();
app.ApplyDbMigrations();
app.Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
