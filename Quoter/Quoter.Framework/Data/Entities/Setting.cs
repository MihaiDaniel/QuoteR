namespace Quoter.Framework.Data.Entities
{
	/// <summary>
	/// Holds a single application setting
	/// </summary>
	public class Setting
	{
		public int SettingId { get; set; }

		public string Name { get; set; }

		public string Value { get; set; }
	}
}
