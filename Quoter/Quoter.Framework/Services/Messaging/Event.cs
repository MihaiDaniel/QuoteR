namespace Quoter.Framework.Services.Messaging
{
	/// <summary>
	/// Contains events used for the messaging service <see cref="IMessagingService"/>
	/// </summary>
	public static class Event
	{
		/// <summary>
		/// Event that signals that the application language has changed
		/// </summary>
		public const string LanguageChanged = "LanguageChanged";
		public const string NotificationIntervalChanged = "NotificationIntervalChanged";
		public const string ShowCollectionsBasedOnLanguageChanged = "ShowCollectionsBasedOnLanguage";
		/// <summary>
		/// Event that signals that the notification type was changed from (AlwaysOn/Popup).
		/// </summary>
		public const string NotificationTypeChanged = "NotificationTypeChanged";
		public const string ThemeChanged = "ThemeChanged";
		/// <summary>
		/// Event that signals the user requested a quote, or a new quote window must be opened.
		/// Any other quote window opened should close when such event occurs, to prevent having
		/// more than 1 quote window open at a time
		/// </summary>
		public const string OpeningQuoteWindow = "OpeningQuoteWindow";
		/// <summary>
		/// Event that signals the user requested a quote or that the notification timer has elapsed
		/// This should normally be send, when there is already a quote window opened in alwaysOn notification type.
		/// It would signal the opened quote window to change quotes.
		/// </summary>
		public const string RequestDisplayNewQuote = "RequestDisplayNewQuote";

		public const string ExportSucessfull = "ExportSucessfull";

		public const string ExportFailed = "ExportFailed";

		public const string ImportSuccesfull = "ImportSuccesfull";

		public const string ImportFailed = "ImportFailed";
		/// <summary>
		/// Event that signals when a form is opening
		/// </summary>
		public const string OpeningForm = "OpeningForm";

		public const string ImportInProgress = "ImportStarted";

		public const string ExportInProgress = "ExportInProgress";

	}
}
