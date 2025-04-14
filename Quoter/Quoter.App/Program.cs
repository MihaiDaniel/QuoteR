using Microsoft.EntityFrameworkCore;
using Quoter.App.Forms;
using Quoter.App.FormsControllers.EditQuotes;
using Quoter.App.FormsControllers.FavouriteQuotes;
using Quoter.App.FormsControllers.Manage;
using Quoter.App.FormsControllers.QuoteController;
using Quoter.App.FormsControllers.Reader;
using Quoter.App.FormsControllers.Settings;
using Quoter.App.FormsControllers.Welcome;
using Quoter.App.Services;
using Quoter.App.Services.BackgroundJobs;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services.Forms;
using Quoter.App.Views;
using Quoter.Framework.Data;
using Quoter.Framework.Data.Repositories;
using Quoter.Framework.Services;
using Quoter.Framework.Services.Api;
using Quoter.Framework.Services.DependencyInjection;
using Quoter.Framework.Services.ImportExport;
using Quoter.Framework.Services.ImportExport.ImportStrategies;
using Quoter.Framework.Services.Messaging;
using Quoter.Framework.Services.AppSettings;
using Quoter.Framework.Services.Versioning;
using System.Resources;
using Quoter.App.Helpers.Extensions;
using Quoter.Framework.Services.Registration;
using Quoter.App.Forms.Manage;

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
			try
			{
				// To customize application configuration such as set high DPI settings or default font,
				// see https://aka.ms/applicationconfiguration.
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.SetHighDpiMode(HighDpiMode.DpiUnawareGdiScaled);
				ApplicationConfiguration.Initialize();

				// By default this will be true. Still not working, maybe try to sign the app
				//if ((bool)Properties.Settings.Default["IsUpgradeRequired"] == true)
				//{
				//	Properties.Settings.Default.Upgrade(); // If we update the app version user.config will have to be copied to the new version
				//	Properties.Settings.Default.Reload();
				//	Properties.Settings.Default.Save();
				//	//Properties.Settings.Default.IsUpgradeRequired = false;
				//	Properties.Settings.Default.Save();
				//	File.WriteAllText("Log.txt", "IsUpgradeRequired was triggered");
				//}

				DependencyInjectionContainer diContainer = SetupDependencyInjection();

				// Apply migrations & seed the database
				QuoterContext context = diContainer.GetService<QuoterContext>();
				context.Database.Migrate();
				context.Seed();

				QuoterApplicationContext appContext = diContainer.GetService<QuoterApplicationContext>();
				Application.Run(appContext);
			}
			catch (Exception ex)
			{
				string specialFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				File.WriteAllText(Path.Combine(specialFolderPath, "Quoter", "CrashLog.txt"), ex.ToString());
			}
		}

		static DependencyInjectionContainer SetupDependencyInjection()
		{
			ServiceCollection serviceCollection = new();

			// Singleton services
			ResourceManager resourceManager = new("Quoter.App.Resources.Strings", typeof(Program).Assembly);
			serviceCollection.AddSingleton<ResourceManager>(resourceManager);
			serviceCollection.AddSingleton<IStringResources, StringResources>();
			serviceCollection.AddSingleton<IFormsManager, FormsManager>();
			serviceCollection.AddSingleton<IAppSettings, AppSettings>();
			serviceCollection.AddSingleton<IAppConfiguration, AppConfiguration>();
			serviceCollection.AddSingleton<IFormLifecycleService, FormLifecycleService>();
			serviceCollection.AddSingleton<IMessagingService, MessagingService>();
			serviceCollection.AddSingleton<IThemeService, ThemeService>();
			serviceCollection.AddSingleton<IExportService, ExportService>();
			serviceCollection.AddSingleton<IImportService, ImportService>();
			serviceCollection.AddSingleton<IImportStrategyServiceFactory, ImportStrategyServiceFactory>();
			serviceCollection.AddSingleton<DefaultImportStrategyService>();
			serviceCollection.AddSingleton<ReplaceImportStrategyService>();
			serviceCollection.AddSingleton<MergeImportStrategyService>();
			serviceCollection.AddSingleton<ISoundService, SoundService>();
			serviceCollection.AddSingleton<IBackgroundJobsFormsService, BackgroundJobsFormsService>();

			// Main application context
			serviceCollection.AddSingleton<QuoterApplicationContext>();

			// Forms
			serviceCollection.AddTransient<SettingsForm>();
			serviceCollection.AddTransient<QuoteForm>();
			serviceCollection.AddTransient<WelcomeForm>();
			serviceCollection.AddTransient<ManageForm>();
			serviceCollection.AddTransient<MainForm>();
			serviceCollection.AddTransient<ReaderForm>();
			serviceCollection.AddTransient<DialogInputForm>();
			serviceCollection.AddTransient<DialogMessageForm>();
			serviceCollection.AddTransient<DialogExportFinishedForm>();

			// Forms controllers
			serviceCollection.AddTransient<IManageFormController, ManageFormController>();
			serviceCollection.AddTransient<IEditQuotesFormController, EditQuotesFormController>();
			serviceCollection.AddTransient<ISettingsFormController, SettingsFormController>();
			serviceCollection.AddTransient<IQuoteFormController, QuoteFormController>();
			serviceCollection.AddTransient<IFavouriteQuotesFormController, FavouriteQuotesFormController>();
			serviceCollection.AddTransient<IWelcomeFormController, WelcomeFormController>();
			serviceCollection.AddTransient<IReaderFormController, ReaderFormController>();

			// Other transient services
			serviceCollection.AddTransient<IQuoterApplicationService, QuoterApplicationService>();
			serviceCollection.AddTransient<IFormAnimationService, FormAnimationsService>();
			serviceCollection.AddTransient<IFormPositioningService, FormPositioningService>();

			serviceCollection.AddTransient<IQuoteService, QuoteService>();
			serviceCollection.AddTransient<ILogger, Logger>();
			serviceCollection.AddTransient<IWebApiService, WebApiService>();
			serviceCollection.AddTransient<IRegistrationService, RegistrationService>();
			serviceCollection.AddTransient<IVersionService, VersionService>();
			serviceCollection.AddTransient<IUpdateService, UpdateService>();
			serviceCollection.AddTransient<ICollectionRepository, CollectionRepository>();
			serviceCollection.AddTransient<ICommonImportService, CommonImportService>();

			// Database
			serviceCollection.AddTransient<QuoterContext>(GetContextConnectionString());

			return serviceCollection.GetContainer();
		}

		private static string GetContextConnectionString()
		{
			string? connectionString = Properties.Settings.Default["ConnectionString"] as string;
			if (string.IsNullOrEmpty(connectionString))
			{
				string specialFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				string dbFolderPath = Path.Combine(specialFolderPath, "Quoter");
				if (!Directory.Exists(dbFolderPath))
				{
					Directory.CreateDirectory(dbFolderPath);
				}
				connectionString = "Data Source=" + Path.Combine(dbFolderPath, "quoter.app.db");
				Properties.Settings.Default["ConnectionString"] = connectionString;
				Properties.Settings.Default.Save();
			}
			return connectionString;
		}
	}
}