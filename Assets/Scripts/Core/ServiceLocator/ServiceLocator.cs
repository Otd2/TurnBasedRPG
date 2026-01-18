using System;
using System.Collections.Generic;

namespace Core
{
    /// <summary>
    /// Simple service locator for dependency management.
    /// Register services at startup, then access them anywhere via Get<T>().
    /// </summary>
    public sealed class ServiceLocator
    {
        private static ServiceLocator _instance;
        private readonly Dictionary<Type, object> _services = new();
        private bool _isLocked;

        public static ServiceLocator Instance => _instance ??= new ServiceLocator();

        private ServiceLocator() { }

        public void Register<T>(T service) where T : class, IService
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            if (_isLocked)
                throw new InvalidOperationException("ServiceLocator is locked.");

            if (_services.ContainsKey(typeof(T))) 
                throw new InvalidOperationException($"{typeof(T).Name} is already registered.");

            _services[typeof(T)] = service;
            service.Initialize();
        }

        public T Get<T>() where T : class, IService
        {
            if (_services.TryGetValue(typeof(T), out var service))
                return (T)service;

            throw new InvalidOperationException($"{typeof(T).Name} is not registered.");
        }

        public bool TryGet<T>(out T service) where T : class, IService
        {
            if (_services.TryGetValue(typeof(T), out var obj))
            {
                service = (T)obj;
                return true;
            }

            service = null;
            return false;
        }

        public void Lock() => _isLocked = true;
    }
}
