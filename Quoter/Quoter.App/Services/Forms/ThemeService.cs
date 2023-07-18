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
				ForeColor = GetForeColorFromTheme(theme),
				BodyColor = GetBodyColorFromTheme(theme),
				HighlightColor = GetHighlightColorFromTheme(theme),
				PressedColor = GetPressedColorFromTheme(theme),
				Opacity = opacity,
				OpenNotificationAnimation = animationOpen,
				CloseNotificationAnimation = animationClose
			};
		}

		private Color GetForeColorFromTheme(EnumTheme theme)
		{
			if(_settings.IsNightMode)
			{
				return Color.WhiteSmoke;
			}
			else
			{
				return Color.DarkGray;
			}
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

		private Color GetBodyColorFromTheme(EnumTheme theme)
		{
			if (_settings.IsNightMode)
			{
				return Color.FromArgb(50,50,50);
			}

			return theme switch
			{
				_ => Color.FromArgb(250,250,250) // lighter than white smoke
			};
		}

		private Color GetHighlightColorFromTheme(EnumTheme theme)
		{
			return theme switch
			{
				EnumTheme.SlateGray => Color.FromArgb(127, 136, 145),
				EnumTheme.Blue => Color.FromArgb(94, 142, 182),
				EnumTheme.Green => Color.FromArgb(28, 160, 28),
				EnumTheme.Orange => Color.FromArgb(253,163,52),
				EnumTheme.LightCoral => Color.FromArgb(241, 173, 173),
				EnumTheme.IndianRed => Color.FromArgb(221, 120, 120),
				_ => Color.WhiteSmoke
			};
		}

		private Color GetPressedColorFromTheme(EnumTheme theme)
		{
			return theme switch
			{
				EnumTheme.SlateGray => Color.FromArgb(127, 136, 145),
				EnumTheme.Blue => Color.FromArgb(94, 142, 182),
				EnumTheme.Green => Color.FromArgb(28, 160, 28),
				EnumTheme.Orange => Color.FromArgb(253, 163, 52),
				EnumTheme.LightCoral => Color.FromArgb(241, 173, 173),
				EnumTheme.IndianRed => Color.FromArgb(221, 120, 120),
				_ => Color.WhiteSmoke
			};
		}
	}
}
