using Quoter.Framework.Enums;

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
				default: throw new NotImplementedException();
			}
		}
	}
}
