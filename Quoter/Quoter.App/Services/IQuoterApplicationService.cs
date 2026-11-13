using Quoter.Framework.Enums;

namespace Quoter.App.Services
{
	/// <summary>
	/// Interface for service handling different features for <see cref="QuoterApplicationContext"/>
	/// </summary>
	/// <remarks>
	/// <see cref="QuoterApplicationService"/>
	/// </remarks>
	public interface IQuoterApplicationService
	{
		/// <summary>
		/// Enqueues a background job that will register the application if it's not registered
		/// </summary>
		void EnqueueBackgroundJobAppRegistration();

		/// <summary>
		/// Enqueues a background job that will try to update the application
		/// </summary>
		/// <remarks>
		/// Update will be done according to the user's setting (<see cref="EnumAutoUpdate"/>)
		/// </remarks>
		[Obsolete("Autoupdate is disabled from server. This method is no longer used. Use store update instead")]
		void EnqueueBackgroundJobAppUpdate();

		bool IsAnyUpdateApplied();

		bool ShowUpdateAppliedNotification();

		/// <summary>
		/// Displays the quote notification window with a new random quote (or signals the window 
		/// to show another quote if is already open)
		/// </summary>
		void ShowRandomQuoteInNotificationWindow();

		/// <summary>
		/// Displays a quote notification window with a welcome message
		/// </summary>
		void ShowWelcomeNotificationWindow();
	}
}
