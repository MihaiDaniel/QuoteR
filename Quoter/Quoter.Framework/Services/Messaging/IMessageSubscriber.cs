namespace Quoter.Framework.Services.Messaging
{
	/// <summary>
	/// Interface implemented by subscribers to the <see cref="IMessagingService"/>
	/// </summary>
	public interface IMessagingSubscriber
	{
		/// <summary>
		/// Method called by <see cref="IMessagingService"/> when an event was sent by any component.
		/// </summary>
		/// <param name="message">The message or name of the event</param>
		/// <param name="argument">Optional argument</param>
		void OnMessageEvent(string message, object? argument);
	}
}
