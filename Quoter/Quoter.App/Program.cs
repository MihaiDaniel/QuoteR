using Microsoft.EntityFrameworkCore;
using Quoter.App.Forms;
using Quoter.App.FormsControllers.EditQuotes;
using Quoter.App.FormsControllers.FavouriteQuotes;
using Quoter.App.FormsControllers.Manage;
using Quoter.App.FormsControllers.QuoteController;
using Quoter.App.FormsControllers.Settings;
using Quoter.App.Helpers;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services.Forms;
using Quoter.App.Views;
using Quoter.Framework.Data;
using Quoter.Framework.Services;
using Quoter.Framework.Services.DependencyInjection;
using Quoter.Framework.Services.ImportExport;
using Quoter.Framework.Services.Messaging;
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
			Application.SetHighDpiMode(HighDpiMode.DpiUnawareGdiScaled);
			ApplicationConfiguration.Initialize();

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
			
			serviceCollection.AddSingleton<IStringResources, StringResources>();
			serviceCollection.AddSingleton<IFormsManager, FormsManager>();
			serviceCollection.AddSingleton<ISettings, Settings>();
			serviceCollection.AddSingleton<IFormLifecycleService, FormLifecycleService>();
			serviceCollection.AddSingleton<IMessagingService, MessagingService>();
			serviceCollection.AddSingleton<IThemeService, ThemeService>();
			serviceCollection.AddSingleton<IExportService, ExportService>();
			serviceCollection.AddSingleton<IImportService, ImportService>();
			serviceCollection.AddSingleton<ISoundService, SoundService>();
			serviceCollection.AddSingleton<QuoterApplicationContext>();

			serviceCollection.AddTransient<SettingsForm>();
			serviceCollection.AddTransient<QuoteForm>();
			serviceCollection.AddTransient<WelcomeForm>();
			serviceCollection.AddTransient<ManageForm>();
			serviceCollection.AddTransient<DialogInputForm>();
			serviceCollection.AddTransient<DialogMessageForm>();

			serviceCollection.AddTransient<IQuoteService, QuoteService>();
			serviceCollection.AddTransient<ILogger, Logger>();

			serviceCollection.AddTransient<IManageFormController, ManageFormController>();
			serviceCollection.AddTransient<IEditQuotesFormController, EditQuotesFormController>();
			serviceCollection.AddTransient<ISettingsFormController, SettingsFormController>();
			serviceCollection.AddTransient<IQuoteFormController, QuoteFormController>();
			serviceCollection.AddTransient<IFavouriteQuotesFormController, FavouriteQuotesFormController>();

			serviceCollection.AddTransient<IFormAnimationService, FormAnimationsService>();
			serviceCollection.AddTransient<IFormPositioningService, FormPositioningService>();

			serviceCollection.AddTransient<QuoterContext>(GetContextConnectionString());

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