﻿using System.Reflection;

namespace Quoter.Framework.Services.DependencyInjection
{
	/// <summary>
	/// Custom implementation of a dependency injection container.
	/// Does not handle more complicated stuff as check for circular dependencies
	/// </summary>
	public class DependencyInjectionContainer
	{
		private readonly Dictionary<Type, ServiceDescriptor> _registeredServices;

		public DependencyInjectionContainer(Dictionary<Type, ServiceDescriptor> registeredServices)
		{
			_registeredServices = registeredServices;
		}

		/// <summary>
		/// Resolve and return a service of type <typeparamref name="T"/> as it was registered.
		/// You can specify any additional optional parameters in <paramref name="arrParameters"/> that
		/// the object expects
		/// </summary>
		/// <typeparam name="T">Type of object to resolve</typeparam>
		/// <param name="arrParameters">Optional parameters that the service might expect (such as poco's)</param>
		/// <returns>The resolved service</returns>
		public T GetService<T>(params object[] arrParameters)
		{
			return (T)GetService(typeof(T), arrParameters);
		}

		private object GetService(Type type, params object[] arrParameters)
		{
			ServiceDescriptor? serviceDescriptor;
			_registeredServices.TryGetValue(type, out serviceDescriptor);

			if (serviceDescriptor == null)
			{
				throw new ArgumentException($"Type not found in serviceCollection: {type}");
			}

			if (serviceDescriptor.Implementation != null)
			{
				return serviceDescriptor.Implementation;
			}
			else
			{
				return ResolveForServiceDescriptor(serviceDescriptor, arrParameters);
			}
		}

		private object ResolveForServiceDescriptor(ServiceDescriptor serviceDescriptor, params object[] arrParameters)
		{
			Type instanceType = serviceDescriptor.ImplementationType;

			// If we have a special Argument for some class, add it at the beginning of arrParameters
			// For example for the dbContext where we specify a string parameter (connection string)
			// that won't change through the lifetime of the application
			if (serviceDescriptor.Argument != null)
			{
				if (arrParameters != null && arrParameters.Length > 0)
				{
					object[] newParams = new object[arrParameters.Length + 1];
					newParams[0] = serviceDescriptor.Argument;
					for (int i = 0; i < newParams.Length; i++)
					{
						newParams[i + 1] = arrParameters[i];
					}
				}
				arrParameters = new object[1] { serviceDescriptor.Argument };
			}

			// We get the most probable constructor
			ConstructorInfo constructorInfo = ResolveConstructorForParameters(instanceType, arrParameters);

			// Resolve recursively the parameters types
			IEnumerable<Type> parmetersTypes = constructorInfo.GetParameters().Select(p => p.ParameterType);
			List<object> lstParametersResolved = new();
			foreach (Type parameterType in parmetersTypes)
			{
				// if any type matches one of the arrParameters we resolve with that one
				if (arrParameters is not null && arrParameters.Length > 0 && arrParameters.Any(p => p.GetType() == parameterType))
				{
					lstParametersResolved.Add(arrParameters.First(p => p.GetType() == parameterType));
				}
				// otherwise we resolve them recursively
				else
				{
					lstParametersResolved.Add(GetService(parameterType));
				}
			}
			// Order parameters in the order we expect for the constructor chosen
			lstParametersResolved = OrderConstructorParameters(lstParametersResolved, parmetersTypes);

			object? instance = Activator.CreateInstance(instanceType, lstParametersResolved.ToArray());

			if (instance == null)
			{
				throw new ArgumentException($"Could not create instance of {instanceType}");
			}

			if (serviceDescriptor.Lifetime == EnumServiceLifetime.Singleton)
			{
				serviceDescriptor.Implementation = instance;
			}

			return instance;
		}

		/// <summary>
		/// Order the <paramref name="lstParametersResolved"/> like we have in the <paramref name="parmetersTypes"/>
		/// so we can feed them to the constructor in the correct order
		/// </summary>
		private List<object> OrderConstructorParameters(List<object> lstParametersResolved, IEnumerable<Type> parmetersTypes)
		{
			List<object> lstParametersResolvedOrdered = new List<object>();
			foreach (Type parameterType in parmetersTypes)
			{
				lstParametersResolvedOrdered.Add(lstParametersResolved.First(pr => parameterType.IsAssignableFrom(pr.GetType())
																			|| pr.GetType() == parameterType));
			}
			return lstParametersResolvedOrdered;
		}

		private ConstructorInfo ResolveConstructorForParameters(Type instanceType, object[] arrParameters)
		{
			ConstructorInfo[] arrConstructorInfo = instanceType.GetConstructors();

			// If not any particular parameters are specified just get 
			// the first constructor that has the least parameters.
			if (arrParameters is null || arrParameters.Length == 0)
			{
				return arrConstructorInfo
					.OrderBy(ci => ci.GetParameters().Length).First();
			}

			// Otherwise get the first constructor that matches the parameters specified in arrParameters
			foreach (ConstructorInfo constructorInfo in arrConstructorInfo)
			{
				IEnumerable<Type> parmetersTypes = constructorInfo.GetParameters().Select(p => p.ParameterType);

				if (arrParameters.All(p => parmetersTypes.Contains(p.GetType())))
				{
					return constructorInfo;
				}

			}
			string strParameters = "";
			if (arrParameters != null && arrParameters.Length > 0)
			{
				foreach (var param in arrParameters)
				{
					strParameters += param.GetType() + " ";
				}
			}
			// Throw if none found
			throw new ArgumentException($"No constructor for type :{instanceType} found with specified expected parameters types {strParameters}.");
		}
	}
}
