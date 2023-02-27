using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Quoter.App.Forms;
using Quoter.App.FormsControllers;
using Quoter.App.Helpers;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services.Forms;
using Quoter.App.Views;
using Quoter.Framework;
using Quoter.Framework.Data;
using Quoter.Framework.Services;
using Quoter.Framework.Services.DependencyInjection;
using System.Resources;

namespace Quoter.App
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			ApplicationConfiguration.Initialize();

			Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ro-RO");

			DependencyInjectionContainer diContainer = SetupDependencyInjection();
			
			// Apply migrations
			QuoterContext context = diContainer.GetService<QuoterContext>();
			context.Database.Migrate();

			QuoterApplicationContext appContext = diContainer.GetService<QuoterApplicationContext>();
			Application.Run(appContext);
		}

		static DependencyInjectionContainer SetupDependencyInjection()
		{
			ServiceCollection serviceCollection = new();

			ResourceManager resourceManager = new("Quoter.App.Resources.Strings", typeof(Program).Assembly);
			serviceCollection.AddSingleton<ResourceManager>(resourceManager);
			serviceCollection.AddSingleton<QuoterApplicationContext>();
			serviceCollection.AddSingleton<IStringResources, StringResources>();
			serviceCollection.AddSingleton<IMemoryCache, MemoryCache>();
			serviceCollection.AddSingleton<IFormsManager, FormsManager>();
			serviceCollection.AddSingleton<ISettings, Settings>();
			serviceCollection.AddSingleton<IFormLifecycleService, FormLifecycleService>();
			serviceCollection.AddSingleton<IRepository, Repository>();

			serviceCollection.AddTransient<SettingsForm>();
			serviceCollection.AddTransient<QuoteForm>();
			serviceCollection.AddTransient<WelcomeForm>();
			serviceCollection.AddTransient<ManageQuotesForm>();
			serviceCollection.AddTransient<DialogInputForm>();
			serviceCollection.AddTransient<DialogMessageForm>();

			serviceCollection.AddTransient<IEditQuotesFormController, EditQuotesFormController>();
			serviceCollection.AddTransient<ISettingsFormController, SettingsFormController>();

			serviceCollection.AddTransient<IFormAnimationService, FormAnimationsService>();
			serviceCollection.AddTransient<IFormPositioningService, FormPositioningService>();

			QuoterContext context = new(GetContextConnectionString());
			serviceCollection.AddSingleton<QuoterContext>(context);

			return serviceCollection.GetContainer();
		}

		private static string GetContextConnectionString()
		{
			string? connectionString = Properties.Settings.Default[Const.Setting.ConnectionString] as string;
			if (string.IsNullOrEmpty(connectionString))
			{
				connectionString = "Data Source=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "quoter.db");
				Properties.Settings.Default[Const.Setting.ConnectionString] = connectionString;
				Properties.Settings.Default.Save();
			}
			return connectionString;
		}
	}
}