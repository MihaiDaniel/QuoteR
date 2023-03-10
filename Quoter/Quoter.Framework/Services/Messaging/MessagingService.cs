using System.Collections.Concurrent;

namespace Quoter.Framework.Services.Messaging
{
	/// <summary>
	/// Default implementation of <see cref="IMessagingService"/>
	/// </summary>
	public class MessagingService : IMessagingService
	{
		private readonly object _lock = new object();
		private readonly ILogger _logger;

		private readonly List<IMessagingSubscriber> _lstSubscribers;
		private readonly ConcurrentDictionary<string, object> _dicPostedAnnouncements;


		public MessagingService(ILogger logger)
		{
			_logger = logger;
			_lstSubscribers = new List<IMessagingSubscriber>();
			_dicPostedAnnouncements = new ConcurrentDictionary<string, object>();
		}

		/// <inheritdoc/>
		public PostedAnnouncement PostAnnouncement<T>(string message, T value)
		{
			bool isAdded = _dicPostedAnnouncements.TryAdd(message, value);
			if (!isAdded)
			{
				bool gotValue = _dicPostedAnnouncements.TryGetValue(message, out object oldValue);
				if (gotValue)
				{
					_dicPostedAnnouncements.TryUpdate(message, value , oldValue);
				}
			}
			SendMessage(message, value);
			return new PostedAnnouncement(_dicPostedAnnouncements, message);
		}

		/// <inheritdoc/>
		public Announcement<T>? FindAnnouncement<T>(string message)
		{
			if (_dicPostedAnnouncements.ContainsKey(message))
			{
				object? value = _dicPostedAnnouncements.GetValueOrDefault(message);
				if (value != null && value.GetType() == typeof(T))
				{
					return new Announcement<T>((T)value);
				}
				else
				{
					return new Announcement<T>(default);
				}
			}
			else
			{
				return null;
			}
		}

		/// <inheritdoc/>
		public bool ExistsAnnouncement(string message)
		{
			return _dicPostedAnnouncements.ContainsKey(message);
		}


		/// <inheritdoc/>
		public void SendMessage(string message, object? argument)
		{
			// lst of subscribers might be modified by OnMessageEvent, so create a list of
			// actions instead and invoke them all.
			List<Action> lstAction = new();

			lock (_lock)
			{
				foreach (IMessagingSubscriber subscriber in _lstSubscribers)
				{
					lstAction.Add(() => { subscriber.OnMessageEvent(message, argument); });
				}
			}
			
			foreach(Action action in lstAction)
			{
				try
				{
					action.Invoke();
				}
				catch(Exception ex)
				{
					_logger.Error(ex);
				}
			}
		}

		/// <inheritdoc/>
		public void Subscribe(IMessagingSubscriber subscriber)
		{
			IMessagingSubscriber? existingSubscriber = _lstSubscribers.FirstOrDefault(s => s == subscriber);
			if (existingSubscriber == null)
			{
				lock (_lock)
				{
					_lstSubscribers.Add(subscriber);
				}
			}
		}

		/// <inheritdoc/>
		public void Unsubscribe(IMessagingSubscriber subscriber)
		{
			if (_lstSubscribers.Contains(subscriber))
			{
				lock (_lock)
				{
					_lstSubscribers.Remove(subscriber);
				}
			}
		}
	}
}
