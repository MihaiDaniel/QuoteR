using Quoter.App.Forms;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Views;
using Quoter.Framework;
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

			serviceCollection.AddTransient<SettingsForm>();
			serviceCollection.AddTransient<MessageForm>();
			serviceCollection.AddTransient<WelcomeForm>();

			serviceCollection.AddTransient<IFormAnimationService, FormAnimationsService>();
			serviceCollection.AddTransient<IFormPositioningService, FormPositioningService>();

			return serviceCollection.GetContainer();
		}
	}
}