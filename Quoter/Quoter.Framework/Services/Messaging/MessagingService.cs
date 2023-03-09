namespace Quoter.Framework.Services.Messaging
{
	/// <summary>
	/// Default implementation of <see cref="IMessagingService"/>
	/// </summary>
	public class MessagingService : IMessagingService
	{
		private readonly List<IMessagingSubscriber> _lstSubscribers;

		public MessagingService()
		{
			_lstSubscribers = new List<IMessagingSubscriber>();
		}

		/// <inheritdoc/>
		public void SendMessage(string message, object? argument)
		{
			// lst of subscribers might be modified by OnMessageEvent, so create a list of
			// actions instead and invoke them all
			List<Action> lstAction = new();

			foreach(IMessagingSubscriber subscriber in _lstSubscribers)
			{
				lstAction.Add(() => { subscriber.OnMessageEvent(message, argument); });
				//subscriber.OnMessageEvent(message, argument);
			}
			lstAction.ForEach(a => a.Invoke());
		}

		/// <inheritdoc/>
		public void Subscribe(IMessagingSubscriber subscriber)
		{
			// Check for type to not have memory leaks. A better approach would be 
			// to implement an ubsubscribe method
			// Todo remove commented code and above 
			IMessagingSubscriber? existingSubscriber = _lstSubscribers.FirstOrDefault(s => s == subscriber);
			if(existingSubscriber == null)
			{
				_lstSubscribers.Add(subscriber);
			}
			else
			{
				//_lstSubscribers.Remove(existingSubscriber);
				//_lstSubscribers.Add(subscriber);
			}
		}

		/// <inheritdoc/>
		public void Unsubscribe(IMessagingSubscriber subscriber)
		{
			if(_lstSubscribers.Contains(subscriber))
			{
				_lstSubscribers.Remove(subscriber);
			}
		}
	}
}
