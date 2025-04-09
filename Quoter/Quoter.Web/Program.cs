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

string dirLocalAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Quoter");
string sqliteDbName = "quoter.web.db";
string sqliteDbLogsName = "quoter.web.logs.db";
if (!Directory.Exists(dirLocalAppData))
{
	Directory.CreateDirectory(dirLocalAppData);
}

Log.Logger = new LoggerConfiguration()
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

	builder.Services.AddDatabaseDeveloperPageExceptionFilter();

	#endregion Database

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

	#region Quoter.Web services

	builder.Services.AddScoped<IStringLocalizer, StringLocalizer<SharedResource>>();
	builder.Services.Configure<UsersConfiguration>(builder.Configuration.GetSection(UsersConfiguration.JsonKey));
	builder.Services.AddScoped<IFileVersionsService, FileVersionsService>();
	builder.Services.AddScoped<IAppVersionService, AppVersionService>();

	#endregion Quoter.Web services

	WebApplication app = builder.Build();

	#region Middleware pipeline

	app.UseSerilogRequestLogging();
	
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
	app.UseRequestLocalization();

	#endregion Middleware pipeline

	app.Run();
}
catch(Exception ex)
{
	Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
	Log.CloseAndFlush();
}


