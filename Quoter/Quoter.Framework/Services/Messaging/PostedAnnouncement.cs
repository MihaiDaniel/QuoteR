﻿using System.Collections.Concurrent;

namespace Quoter.Framework.Services.Messaging
{
	/// <summary>
	/// Represents a posted announcement in the <see cref="IMessagingService"/>. This object
	/// can be used to remove the announcement from the <see cref="IMessagingService"/>
	/// </summary>
	public class PostedAnnouncement
	{
		private readonly ConcurrentDictionary<string, object> _dic;
		private readonly string _announcement;

		public PostedAnnouncement(ConcurrentDictionary<string, object> dic, string announcement)
		{
			_dic = dic;
			_announcement = announcement;
		}

		/// <summary>
		/// Remove the announcement
		/// </summary>
		public bool Remove()
		{
			return _dic.Remove(_announcement, out object _);
		}
	}
}
