using System.Resources;

namespace Quoter.App.Services
{
	/// <summary>
	/// Default implementation of <see cref="IStringResources"/>
	/// </summary>
	public class StringResources : IStringResources
	{
		private readonly ResourceManager _resourceManager;

		public StringResources(ResourceManager resourceManager)
		{
			_resourceManager = resourceManager;
		}

		/// <inheritdoc/>
		public string this[string name] 
		{
			get
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new ArgumentNullException(nameof(name));
				}
				return _resourceManager.GetString(name) ?? throw new ArgumentException($"String not found: {name}");
			}
		}

		/// <inheritdoc/>
		public string this[string name, params string[] param]
		{
			get
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new ArgumentNullException(nameof(name));
				}
				string localizedString = _resourceManager.GetString(name) ?? throw new ArgumentException($"String not found: {name}");
				for(int index = 0; index < param.Length; index++)
				{
					string parameterInString = $"{{{index}}}";
					localizedString = localizedString.Replace(parameterInString, param[index]);
				}
				return localizedString;
			}
		}
	}
}
