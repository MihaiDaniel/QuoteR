using Quoter.Framework.Enums;

namespace Quoter.App.Models
{
	/// <summary>
	/// Item model to display update modes that can be chosen by user
	/// </summary>
	public class UpdateModeItem
	{
		/// <summary>
		/// Mode how we can update the application
		/// </summary>
		public EnumAutoUpdate UpdateMode { get; set; }

		/// <summary>
		/// The localized text displayed for this item
		/// </summary>
		public string DisplayName { get; set; }
	}
}
