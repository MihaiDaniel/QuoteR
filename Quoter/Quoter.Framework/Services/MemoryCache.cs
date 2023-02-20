namespace Quoter.Framework.Services
{
	public class MemoryCache : IMemoryCache
	{
		private readonly Dictionary<string, object?> _cache;

		public MemoryCache()
		{
			_cache = new Dictionary<string, object?>();
		}

		public void TryAdd<T>(string key, T value)
		{
			if(!_cache.ContainsKey(key))
			{
				_cache.Add(key, value);
			}
		}

		public bool Contains(string key)
		{
			return _cache.ContainsKey(key);
		}

		public T GetOrDefault<T>(string key)
		{
			_cache.TryGetValue(key, out object? value);
			if(value == null)
			{
				return default(T);
			}
			else if(value.GetType() == typeof(T))
			{
				return (T)value;
			}
			else
			{
				throw new InvalidOperationException($"Type {value.GetType()} of value {value} is not the expected returned type {typeof(T)}");
			}
		}
	}
}
