namespace Quoter.Framework.Services.Messaging
{
	/// <summary>
	/// Contains events used for the messaging service <see cref="IMessagingService"/>
	/// </summary>
	public static class Event
	{
		public const string LanguageChanged = "LanguageChanged";
		public const string NotificationIntervalChanged = "NotificationIntervalChanged";
		public const string ShowCollectionsBasedOnLanguageChanged = "ShowCollectionsBasedOnLanguage";
		public const string NotificationTypeChanged = "NotificationTypeChanged";
		public const string ThemeChanged = "ThemeChanged";
		/// <summary>
		/// Event that signals that the show notification quote timer has elapsed.
		/// For a popup type notification, it should show a popup notification.
		/// For a alwaysOn type notification it should signal the opened quote form
		/// to change the quote, or open it if it was closed
		/// </summary>
		public const string NotificationTimerElapsed = "NotificationTimerElapsed";
		/// <summary>
		/// Event that signals the user requested a quote, or a new quote window must be opened.
		/// Any other quote window opened should close when such event occurs, to prevent having
		/// more than 1 quote window open at a time
		/// </summary>
		public const string OpeningQuoteWindow = "OpeningQuoteWindow";
		/// <summary>
		/// Event that signals the user requested a quote. This should normally be send, when
		/// there is already a quote window opened in alwaysOn notification type.
		/// It would signal the opened quote window to change quotes.
		/// </summary>
		public const string ShowQuoteButtonEvent = "ShowQuoteButtonEvent";

		public const string ExportSucessfull = "ExportSucessfull";

		public const string ExportFailed = "ExportFailed";

		public const string ImportSuccesfull = "ImportSuccesfull";

		public const string ImportFailed = "ImportFailed";
	}
}
