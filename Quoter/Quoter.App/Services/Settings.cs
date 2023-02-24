namespace Quoter.App.Services
{
	public class Settings : ISettings
	{
		private readonly object _lock = new object();

		/// <inheritdoc/>
		public T Get<T>(string key)
		{
			lock (_lock)
			{
				ValidateSetting<T>(key);
				return (T)Properties.Settings.Default[key];
				
			}
		}

		/// <inheritdoc/>
		public void Set<T>(string key, T value)
		{
			lock(_lock)
			{
				ValidateSetting<T>(key);
				if (Properties.Settings.Default[key] != (object?)value)
				{
					Properties.Settings.Default[key] = value;
					Properties.Settings.Default.Save();
				}
			}
		}

		private void ValidateSetting<T>(string key)
		{
			if (Properties.Settings.Default[key] == null)
			{
				throw new ArgumentException($"Setting {key} does not exist");

			}
			if (Properties.Settings.Default[key].GetType() != typeof(T))
			{
				throw new ArgumentException($"Setting {key} is not of type {typeof(T)}");
			}
		}
	}
}
