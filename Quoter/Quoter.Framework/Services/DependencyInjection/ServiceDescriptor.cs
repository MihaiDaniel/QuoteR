using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services.DependencyInjection
{
	public class ServiceDescriptor
	{
		public Type Type { get; private set; }

		public Type? InterfaceType { get; private set; }

		public object Implementation { get; private set; }

		public EnumServiceLifetime Lifetime { get; private set; }

		public ServiceDescriptor(Type interfaceType, object implementation, EnumServiceLifetime serviceLifetime)
		{
			Type = implementation.GetType();
			InterfaceType = interfaceType;
			Implementation = implementation;
			Lifetime = serviceLifetime;
		}

		public ServiceDescriptor(object implementation, EnumServiceLifetime serviceLifetime)
		{
			Type = implementation.GetType();
			InterfaceType = null;
			Implementation = implementation;
			Lifetime = serviceLifetime;
		}
	}
}
