using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using Quoter.Web.Extensions;
using Quoter.Web.Models.Configuration;
using Quoter.Web.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Quoter.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Quoter.Web;
using Serilog;
using Serilog.Events;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using System.Net;

string dirLocalAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Quoter");
string sqliteDbName = "quoter.web.db";
string sqliteDbLogsName = "quoter.web.logs.db";
if (!Directory.Exists(dirLocalAppData))
{
	Directory.CreateDirectory(dirLocalAppData);
}

Serilog.Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
	.MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
	.MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
	.MinimumLevel.Warning()
	.WriteTo.Console()
	.WriteTo.File(Path.Combine(dirLocalAppData, "Logs.txt"))
	.WriteTo.SQLite(Path.Combine(dirLocalAppData, sqliteDbLogsName)) // Use different db because it caused file locking issues
	.CreateLogger();

try
{
	WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
	

	builder.Services.AddSerilog();
	// To read from appsettings
	//builder.Services.AddSerilog((services, loggerConfiguration) =>
	//	loggerConfiguration.ReadFrom.Configuration(builder.Configuration)
	//						.ReadFrom.Services(services)
	//						.Enrich.FromLogContext()
	//						.WriteTo.SQLite(Path.Combine(dirLocalAppData,"quoter.web.logs.db")));

	builder.Services.AddMemoryCache();
	builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
	builder.Services.AddInMemoryRateLimiting();
	builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

	builder.Services.AddRazorPages()
		.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
		.AddDataAnnotationsLocalization();
	builder.Services.AddControllers();
	builder.Services.AddHttpContextAccessor();

	#region Localization

	builder.Services.AddLocalization(options =>
	{
		options.ResourcesPath = "Resources";
	});
	builder.Services.Configure<RequestLocalizationOptions>(options =>
	{
		var supportedCultures = new[] { "en-US", "fr-FR", "ro-RO" };
		options.SetDefaultCulture(supportedCultures[0])
			.AddSupportedCultures(supportedCultures)
			.AddSupportedUICultures(supportedCultures);
		options.DefaultRequestCulture = new RequestCulture("en-US");
	});

	#endregion Localization

	builder.Services.AddMvc();

	#region Database

	string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

	// PostgreSQL database
	//string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
	//builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
	//	options.UseNpgsql(connectionString));

	// SQLite database
	if (string.IsNullOrEmpty(connectionString)) // Set a default path if not set in appsettings file
	{
		connectionString = $"Data Source={Path.Combine(dirLocalAppData, sqliteDbName)}";
	}
	builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
		options.UseSqlite(connectionString));

	builder.Services.AddDbContextPool<LogsDbContext>(options =>
		options.UseSqlite($"Data Source={Path.Combine(dirLocalAppData, sqliteDbLogsName)}"));

	builder.Services.AddDatabaseDeveloperPageExceptionFilter();

	#endregion Database

	builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
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
	}).AddEntityFrameworkStores<ApplicationDbContext>(); ;

	//builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
	//{
	//	options.SignIn.RequireConfirmedAccount = false;
	//	options.SignIn.RequireConfirmedEmail = false;
	//	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	//	options.Lockout.MaxFailedAccessAttempts = 5;
	//	options.User.RequireUniqueEmail = true;
	//	options.Password.RequireDigit = false;
	//	options.Password.RequireNonAlphanumeric = false;
	//	options.Password.RequiredLength = 4;
	//	options.Password.RequiredUniqueChars = 0;
	//	options.Password.RequireUppercase = false;
	//	options.Password.RequireLowercase = false;

	//})
	//	.AddEntityFrameworkStores<ApplicationDbContext>();

	builder.Services.AddSwaggerGen(c =>
	{
		c.OperationFilter<SwaggerRegistrationHeaderParameter>();
	});

	#region Quoter.Web services

	builder.Services.AddScoped<IStringLocalizer, StringLocalizer<SharedResource>>();
	builder.Services.Configure<UsersConfiguration>(builder.Configuration.GetSection(UsersConfiguration.JsonKey));
	builder.Services.AddScoped<IFileUploadService, FileUploadService>();
	builder.Services.AddScoped<IAppVersionService, AppVersionService>();

	#endregion Quoter.Web services

	WebApplication app = builder.Build();

	#region Middleware pipeline

	app.UseSerilogRequestLogging();

	app.UseIpRateLimiting();

	app.MigrateDatabase();							// Migrate the database if needed
	app.SeedDatabase(builder.Configuration).Wait(); // Seed data in the database if needed

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
	

	app.UseHttpsRedirection();
	app.UseStaticFiles();

	app.UseRouting();

	// Set the forwared headers to get real IP from Nginx
	ForwardedHeadersOptions forwardedHeadersOptions = new ()
	{
		ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
	};
	// Trust the Docker bridge network and Nginx proxy's IP by default so forwared headers will work
	forwardedHeadersOptions.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("172.17.0.0"), 16));
	forwardedHeadersOptions.KnownProxies.Add(IPAddress.Parse("172.17.0.1"));

	app.UseForwardedHeaders(forwardedHeadersOptions);

	app.UseAuthentication();
	app.UseAuthorization();

	app.MapRazorPages();
	app.MapControllers();
	app.UseRequestLocalization();

	#endregion Middleware pipeline

	app.Run();
}
catch(Exception ex)
{
	Serilog.Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
	Serilog.Log.CloseAndFlush();
}


