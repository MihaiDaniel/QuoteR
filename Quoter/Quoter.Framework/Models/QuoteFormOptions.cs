using Quoter.Framework.Enums;

namespace Quoter.Framework.Models
{
	/// <summary>
	/// Options for the quote form used to know what to display when the form opens
	/// </summary>
	public class QuoteFormOptions
	{
		public long QuoteId { get; set; }

		public string Title { get; set; }

		public string Body { get; set; }

		public string Footer { get; set; }

		public EnumAnimation? OpenAnimation { get; set; }

		public EnumAnimation? CloseAnimation { get; set; }

		/// <summary>
		/// Indicates if user can see the next and previous buttons and is allowed to go to next or previous quotes
		/// </summary>
		/// <remarks>
		/// When displaying any message like the welcome message by default this is false.
		/// It must be explicitly set to true when displaying quotes
		/// </remarks>
		public bool AllowNavigation { get; set; } = false;

		public QuoteFormOptions()
		{
			Title = string.Empty;
			Body = string.Empty;
			Footer = string.Empty;
		}
	}
}
