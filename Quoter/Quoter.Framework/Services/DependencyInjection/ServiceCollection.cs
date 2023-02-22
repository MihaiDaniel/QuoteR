namespace Quoter.Framework.Services.DependencyInjection
{
	public class ServiceCollection
	{
		private readonly Dictionary<Type, ServiceDescriptor> _registeredServices;

		public ServiceCollection()
		{
			_registeredServices = new Dictionary<Type, ServiceDescriptor>();
		}

		public void AddSingleton<TInterface, TImplementation>() where TImplementation : TInterface
		{
			_registeredServices.Add(typeof(TInterface), new ServiceDescriptor(typeof(TInterface), typeof(TImplementation), EnumServiceLifetime.Singleton));
		}

		public void AddSingleton<TImplementation>()
		{
			_registeredServices.Add(typeof(TImplementation), new ServiceDescriptor(typeof(TImplementation), EnumServiceLifetime.Singleton));
		}

		public void AddSingleton<TImplementation>(object implementation)
		{
			if(implementation.GetType() != typeof(TImplementation))
			{
				throw new ArgumentException($"Cannot register service of type {typeof(TImplementation)} with implementation type {implementation.GetType()}");
			}
			_registeredServices.Add(typeof(TImplementation), new ServiceDescriptor(typeof(TImplementation), implementation, EnumServiceLifetime.Singleton));
		}

		public void AddTransient<TInterface, TImplementation>()
		{
			_registeredServices.Add(typeof(TInterface), new ServiceDescriptor(typeof(TInterface), typeof(TImplementation), EnumServiceLifetime.Transient));
		}

		public void AddTransient<TImplementation>()
		{
			_registeredServices.Add(typeof(TImplementation), new ServiceDescriptor(typeof(TImplementation), EnumServiceLifetime.Transient));
		}

		public DependencyInjectionContainer GetContainer()
		{
			DependencyInjectionContainer diContainer = new(_registeredServices);
			AddSingleton<DependencyInjectionContainer>(diContainer); // Also add the container itself
			return diContainer;
		}

	}
}
