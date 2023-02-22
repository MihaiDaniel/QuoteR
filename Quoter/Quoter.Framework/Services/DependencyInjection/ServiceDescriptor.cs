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

		public Type ImplementationType { get; private set; }

		public object? Implementation { get; internal set; }

		public EnumServiceLifetime Lifetime { get; private set; }

		public ServiceDescriptor(Type type, Type implementationType, EnumServiceLifetime serviceLifetime)
		{
			Type = type;
			ImplementationType = implementationType;
			Lifetime = serviceLifetime;
		}

		public ServiceDescriptor(Type type, EnumServiceLifetime serviceLifetime)
		{
			Type = type;
			ImplementationType = type;
			Lifetime = serviceLifetime;
		}

		public ServiceDescriptor(Type type, object implementation, EnumServiceLifetime serviceLifetime)
		{
			Type = type;
			ImplementationType = type;
			Implementation = implementation;
			Lifetime = serviceLifetime;
		}
	}
}
