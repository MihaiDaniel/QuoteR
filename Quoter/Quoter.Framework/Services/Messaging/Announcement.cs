namespace Quoter.Framework.Services.Messaging
{
	public class Announcement<T>
	{
		public T? Value { get; set; }

		public Announcement()
		{
		}

		public Announcement(T value)
		{
			Value = value;
		}
	}
}
