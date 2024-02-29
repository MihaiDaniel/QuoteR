using Quoter.Framework.Data;
using Quoter.Framework.Data.Entities;
using Quoter.Framework.Enums;
using Quoter.Framework.Services.AppSettings;

namespace Quoter.App.Helpers.Extensions
{
	public static class QuoterContextExtensions
	{
		public static void Seed(this QuoterContext context)
		{
			SeedAppSettings(context);
		}

		private static void SeedAppSettings(QuoterContext context)
		{
			TrySeedAppSetting(context, nameof(IAppSettings.IsFirstStart), false);
			TrySeedAppSetting(context, nameof(IAppSettings.IsSetupFinished), false);
			TrySeedAppSetting(context, nameof(IAppSettings.InstallId), "");

			TrySeedAppSetting(context, nameof(IAppSettings.IsPaused), false);

			TrySeedAppSetting(context, nameof(IAppSettings.Language), "en-US");
			TrySeedAppSetting(context, nameof(IAppSettings.FontName), "Calibri");
			TrySeedAppSetting(context, nameof(IAppSettings.FontStyle), "Regular");
			TrySeedAppSetting(context, nameof(IAppSettings.FontSize), 14.0f);
			TrySeedAppSetting(context, nameof(IAppSettings.NotificationIntervalSeconds), 900);
			TrySeedAppSetting(context, nameof(IAppSettings.AutoCloseNotificationSeconds), 30);
			TrySeedAppSetting(context, nameof(IAppSettings.NotificationOpenAnimation), (int)EnumAnimation.FadeInFromBottomRight);
			TrySeedAppSetting(context, nameof(IAppSettings.NotificationCloseAnimation), (int)EnumAnimation.FadeOut);
			TrySeedAppSetting(context, nameof(IAppSettings.NotificationType), (int)EnumNotificationType.Popup);
			TrySeedAppSetting(context, nameof(IAppSettings.NotificationSound), (int)EnumSound.Click);
			TrySeedAppSetting(context, nameof(IAppSettings.ShowWelcomeNotification), true);
			TrySeedAppSetting(context, nameof(IAppSettings.KeepNotificationOpenOnMouseOver), true);

			TrySeedAppSetting(context, nameof(IAppSettings.AutoUpdate), (int)EnumAutoUpdate.Auto);

			string? webApiUrl = Properties.Settings.Default["WebApiUrl"] as string;
			TrySeedAppSetting(context, nameof(IAppSettings.WebApiUrl), webApiUrl);
			TrySeedAppSetting(context, nameof(IAppSettings.RegistrationId), "00000000-0000-0000-0000-000000000000");
			TrySeedAppSetting(context, nameof(IAppSettings.ShowCollectionsBasedOnLanguage), true);
			TrySeedAppSetting(context, nameof(IAppSettings.IsStartWithWindows), true);

			TrySeedAppSetting(context, nameof(IAppSettings.Theme), (int)EnumTheme.SlateGray);
			TrySeedAppSetting(context, nameof(IAppSettings.Opacity), 1.0d);
			TrySeedAppSetting(context, nameof(IAppSettings.IsNightMode), false); // <- not really working properly with winforms
			TrySeedAppSetting(context, nameof(IAppSettings.WindowSize), new Size(800,524));

			context.SaveChanges();
		}

		private static void TrySeedAppSetting<T>(QuoterContext context, string name, T value)
		{
			Setting? setting = context.Settings.FirstOrDefault(s => s.Name == name);
			if(setting is null)
			{
				setting = new Setting()
				{
					Name = name,
					Value = AppSettings.ConvertToString(value)
				};
				context.Settings.Add(setting);
			}
		}
	}
}
