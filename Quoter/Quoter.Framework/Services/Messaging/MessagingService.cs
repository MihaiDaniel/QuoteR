namespace Quoter.Framework.Services.Messaging
{
	/// <summary>
	/// Default implementation of <see cref="IMessagingService"/>
	/// </summary>
	public class MessagingService : IMessagingService
	{
		private readonly List<IMessageSubscriber> _lstSubscribers;

		public MessagingService()
		{
			_lstSubscribers = new List<IMessageSubscriber>();
		}

		/// <inheritdoc/>
		public void SendMessage(string message, object? argument = null)
		{
			foreach(IMessageSubscriber subscriber in _lstSubscribers)
			{
				subscriber.OnMessageEvent(message, argument);
			}
		}

		/// <inheritdoc/>
		public void Subscribe(IMessageSubscriber subscriber)
		{
			// Check for type to not have memory leaks. A better approach would be 
			// to implement an ubsubscribe method
			IMessageSubscriber? existingSubscriber = _lstSubscribers.FirstOrDefault(s => s.GetType() == subscriber.GetType());
			if(existingSubscriber == null)
			{
				_lstSubscribers.Add(subscriber);
			}
			else
			{
				_lstSubscribers.Remove(existingSubscriber);
				_lstSubscribers.Add(subscriber);
			}
		}
	}
}
