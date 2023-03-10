namespace Quoter.Framework.Services.Messaging
{
	/// <summary>
	/// Interface for sending simple messages across the application to various components
	/// </summary>
	public interface IMessagingService
	{
		/// <summary>
		/// Send a message to notify any subscribes of an event. Can have optional object argument
		/// </summary>
		/// <param name="message">Message / Name of the event</param>
		/// <param name="argument">Optional argument</param>
		void SendMessage(string message, object? argument = null);

		/// <summary>
		/// Subscribe to the messaging service to receive notifications when any event occurs
		/// </summary>
		/// <param name="subscriber">The subscriber</param>
		void Subscribe(IMessagingSubscriber subscriber);

		/// <summary>
		/// Unsubscribe from the messaging service. If subscriber is not subscribed this does nothing.
		/// </summary>
		/// <param name="subscriber">The subscriber</param>
		void Unsubscribe(IMessagingSubscriber subscriber);

		/// <summary>
		/// Post an announcement. This will send a message to all subscribers and also maintain the message so
		/// that any other service can access it. Returns an object that allows the sender to remove the posted 
		/// message.
		/// </summary>
		/// <typeparam name="T">Type of value of the message</typeparam>
		/// <param name="message">Message posted</param>
		/// <param name="value">Value of the message</param>
		/// <returns>PostedAnnouncement object allowing the sender to remove the posted announcement</returns>
		PostedAnnouncement PostAnnouncement<T>(string message, T value);

		/// <summary>
		/// Tries to find a particular announcement posted by another service using the <paramref name="message"/>.
		/// Returns null if not found.
		/// </summary>
		/// <typeparam name="T">Type of the Value of the annoucement</typeparam>
		/// <param name="message">Message by which to identify the announcement</param>
		/// <returns>Null if no annoncement found, or an Announcement containing a value</returns>
		Announcement<T>? FindAnnouncement<T>(string message);

		/// <summary>
		/// Tries to find a particular announcement posted by another service using the <paramref name="message"/>.
		/// Returns True if such announcement exist, False otherwise.
		/// </summary>
		bool ExistsAnnouncement(string message);
	}
}
