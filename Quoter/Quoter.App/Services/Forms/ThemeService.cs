using Quoter.App.Helpers;
using Quoter.Framework.Enums;
using Quoter.Framework.Services;

namespace Quoter.App.Services.Forms
{
	public class ThemeService : IThemeService
	{
		private readonly ISettings _settings;

		public ThemeService(ISettings settings)
		{
			_settings = settings;
		}

		public Theme GetCurrentTheme()
		{
			EnumTheme theme = _settings.Theme;
			double opacity = _settings.Opacity;

			EnumAnimation animationOpen = _settings.NotificationOpenAnimation;
			EnumAnimation animationClose = _settings.NotificationCloseAnimation;

			return new Theme()
			{
				TitleColor = GetTitleColorFromTheme(theme),
				BodyColor = Color.WhiteSmoke,
				Opacity = opacity,
				OpenNotificationAnimation = animationOpen,
				CloseNotificationAnimation = animationClose
			};
		}

		private Color GetTitleColorFromTheme(EnumTheme theme)
		{
			return theme switch
			{
				EnumTheme.SlateGray => Color.SlateGray,
				EnumTheme.Blue => Color.SteelBlue,
				EnumTheme.Green => Color.Green,
				EnumTheme.Orange => Color.DarkOrange,
				EnumTheme.LightCoral => Color.LightCoral,
				EnumTheme.IndianRed => Color.IndianRed,
				_ => Color.SlateGray
			};
		}
	}
}
