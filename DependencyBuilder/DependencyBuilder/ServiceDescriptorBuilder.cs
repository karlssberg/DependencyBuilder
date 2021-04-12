using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyBuilder
{
    public class ServiceDescriptorBuilder
    {
        private Implementation _implementation;
        private ServiceLifetime _lifetime;

        public ServiceDescriptorBuilder(Type serviceType, Type implementationType)
        {
            _implementation = new Implementation(serviceType, implementationType);
        }
        
        public ServiceDescriptorBuilder Override(Type dependency, Type implementation)
        {
            _implementation.Override(new Implementation(dependency, implementation));
            
            return this;
        }

        public ServiceDescriptorBuilder Decorate(Type decorator)
        {
            _implementation = new Implementation(_implementation.ServiceType, decorator)
                .Decorate(_implementation);

            return this;
        }

        internal IEnumerable<Type> GetOverriddenTypes() => _implementation.GetOverriddenTypes();

        internal ServiceDescriptor Build() => 
            new (_implementation.ServiceType, _implementation.Factory, _lifetime);

        public ServiceDescriptorBuilder Lifetime(ServiceLifetime serviceLifetime)
        {
            _lifetime = serviceLifetime;

            return this;
        }
    }
}