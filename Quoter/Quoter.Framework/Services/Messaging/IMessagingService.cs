namespace Quoter.Framework.Services.Messaging
{
	/// <summary>
	/// Interface for sending simple messages across the application to various components
	/// </summary>
	public interface IMessagingService
	{
		/// <summary>
		/// Send a message to notify any subscribes of an event
		/// </summary>
		/// <param name="message">Message / Name of the event</param>
		/// <param name="argument">Optional argument</param>
		void SendMessage(string message, object? argument = null);

		/// <summary>
		/// Subscribe to the messaging service to receive notifications when any event occurs
		/// </summary>
		/// <param name="subscriber">The subscriber</param>
		void Subscribe(IMessageSubscriber subscriber);

		/// <summary>
		/// Unsubscribe from the messaging service. If subscriber is not subscribed this does nothing.
		/// </summary>
		/// <param name="subscriber">The subscriber</param>
		void Unsubscribe(IMessageSubscriber subscriber);
	}
}
