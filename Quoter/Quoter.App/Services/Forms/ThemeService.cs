using Quoter.App.Helpers;
using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			EnumTheme theme = (EnumTheme)_settings.Get<int>(Const.Setting.Theme);
			double opacity = _settings.Get<double>(Const.Setting.Opacity);

			EnumAnimation animationOpen = (EnumAnimation)_settings.Get<int>(Const.Setting.NotificationOpenAnimation);
			EnumAnimation animationClose = (EnumAnimation)_settings.Get<int>(Const.Setting.NotificationCloseAnimation);

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
