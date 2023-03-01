using Quoter.Framework.Enums;

namespace Quoter.App.Helpers
{
	public class Theme
	{
		public Color TitleColor { get; set; }

		public Color BodyColor { get; set; }

		public double Opacity { get; set; }

		public EnumAnimation OpenNotificationAnimation { get; set; }

		public EnumAnimation CloseNotificationAnimation { get; set; }
	}
}
