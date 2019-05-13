using System;
using System.Collections.Generic;

namespace OnMenu
{
    /// <summary>
    /// References platform-dependent code
    /// </summary>
    public sealed class ServiceLocator
    {
        /// <summary>
        /// The ServiceLocator instance
        /// </summary>
        static readonly Lazy<ServiceLocator> instance = new Lazy<ServiceLocator>(() => new ServiceLocator());
        /// <summary>
        /// A dictionary which maps common interfaces to native implementations
        /// </summary>
        readonly Dictionary<Type, Lazy<object>> registeredServices = new Dictionary<Type, Lazy<object>>();

        /// <summary>
        /// Static instance of the locator
        /// </summary>
        public static ServiceLocator Instance => instance.Value;

        /// <summary>
        /// Registers an interface
        /// </summary>
        /// <typeparam name="TContract">The contract to establish</typeparam>
        /// <typeparam name="TService">The corresponding service</typeparam>
        public void Register<TContract, TService>() where TService : new()
        {
            registeredServices[typeof(TContract)] =
                new Lazy<object>(() => Activator.CreateInstance(typeof(TService)));
        }

        /// <summary>
        /// Gets the implementation for a given interface
        /// </summary>
        /// <typeparam name="T">The interface to resolve</typeparam>
        /// <returns>The implementation</returns>
        public T Get<T>() where T : class
        {
            Lazy<object> service;
            if (registeredServices.TryGetValue(typeof(T), out service))
            {
                return (T)service.Value;
            }

            return null;
        }
    }
}
