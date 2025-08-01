﻿using Quoter.Framework.Services;
using System.Resources;

namespace Quoter.App.Services
{
	/// <summary>
	/// Default implementation of <see cref="IStringResources"/>
	/// </summary>
	public class StringResources : IStringResources
	{
		private readonly ResourceManager _resourceManager;
		private readonly ILogger _logger;

		public StringResources(ResourceManager resourceManager, ILogger logger)
		{
			_resourceManager = resourceManager;
			_logger = logger;
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
				string? localizedString = null;
				try
				{
					localizedString = _resourceManager.GetString(name);
				}
				catch 
				{
					// we log below if no value is found
				}
				if(localizedString == null)
				{
					_logger.Warn($"No translation found for: {name}");
					return "! " + name;
				}
				else
				{
					return localizedString;
				}
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
				string? localizedString = null;
				try
				{
					localizedString = _resourceManager.GetString(name);
				}
				catch
				{
					// we log below if no value is found
				}
				if (localizedString == null)
				{
					_logger.Warn($"No translation found for: {name}");
					return "! " + name;
				}
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
