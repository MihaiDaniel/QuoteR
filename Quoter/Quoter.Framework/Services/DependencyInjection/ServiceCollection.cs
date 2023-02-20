using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services.DependencyInjection
{
	public class ServiceCollection
	{

		Dictionary<Type, ServiceDescriptor> _registeredServices;

		public ServiceCollection() 
		{
			_registeredServices = new Dictionary<Type, ServiceDescriptor>();
			//_registeredSingletons = new Dictionary<Type, object>();
		}

		public void AddTransient<I, T>(I typeInterface, T implementation)
		{
			_registeredServices.Add(typeof(I), new ServiceDescriptor(typeInterface.GetType(), implementation, EnumServiceLifetime.Transient));
		}

		public void AddTransient<T>(T implementation)
		{
			_registeredServices.Add(typeof(T), new ServiceDescriptor(implementation, EnumServiceLifetime.Transient));
		}

		public void Build()
		{
			foreach(Type type in _registeredServices.Keys)
			{
				Resolve(type);
			}
		}

		private void Resolve(Type type)
		{
			type.GetConstructor(new Type[0]).Invoke(new object?[0]);
		}
	}
}
