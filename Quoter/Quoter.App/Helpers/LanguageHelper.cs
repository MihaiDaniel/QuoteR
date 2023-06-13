using Quoter.Framework.Enums;
using Quoter.Shared.Enums;
using System.Globalization;

namespace Quoter.App.Helpers
{
	public static class LanguageHelper
	{
		public static string GetStringFromLanguage(EnumLanguage language)
		{
			switch (language)
			{
				case EnumLanguage.English:
					return "en-US";
				case EnumLanguage.Romanian:
					return "ro-RO";
				case EnumLanguage.French:
					return "fr-FR";
				default:
					throw new NotImplementedException();
			}
		}

		public static EnumLanguage GetEnumLanguageFromString(string language)
		{
			switch (language)
			{
				case "en-US": return EnumLanguage.English;
				case "ro-RO": return EnumLanguage.Romanian;
				case "fr-FR": return EnumLanguage.French;
				default: throw new NotImplementedException();
			}
		}

		public static string SetCurrentUICultureForCurrentThread()
		{
			CultureInfo ci = CultureInfo.CurrentUICulture;
			switch (ci.Name)
			{
				case "ro-RO":
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("ro-RO");
					return "ro-RO";
				case "fr-FR":
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
					return "fr-FR";
				default:
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
					return "en-US";
			}
		}

		public static void SetCurrentThreadCulture(string culture)
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
			Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
		}
	}
}
