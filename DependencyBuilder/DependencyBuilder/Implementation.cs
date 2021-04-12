using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyBuilder
{
    [DebuggerDisplay("{" + nameof(Type) + "}")]
    internal class Implementation
    {
        internal ICollection<Implementation> Overrides { get; } = new List<Implementation>();

        private Func<IServiceProvider, object> DefaultFactory =>
            provider => provider.GetRequiredService(Type);

        public Implementation(Type serviceType, Type type)
        {
            ServiceType = serviceType;
            Type = type;
        }

        public Type ServiceType { get; }
        
        public Type Type { get; }

        internal IEnumerable<Type> GetOverriddenTypes()
        {
            yield return Type;

            foreach (var type in Overrides.SelectMany(_ => _.GetOverriddenTypes()))
                yield return type;
        }

        internal Implementation Decorate(Implementation implementation)
        {
            if (implementation.ServiceType != ServiceType)
                throw new ArgumentException(
                    "ServiceType mismatch. You can only decorate objects that implement the same service",
                    nameof(implementation));
            
            return Override(implementation);
        }
        
        internal Implementation Override(Implementation implementation)
        {
            Overrides.Add(implementation);

            return this;
        }

        public Func<IServiceProvider, object> Factory =>
            Overrides.Any() 
                ? CustomFactory 
                : DefaultFactory;

        private Func<IServiceProvider, object> CustomFactory
        {
            get
            {
                var argumentTypes = Overrides.Select(_ => _.ServiceType).ToArray();

                var objectFactory = ActivatorUtilities.CreateFactory(Type, argumentTypes);

                var argsFactories = Overrides
                    .Select(dependency => dependency.Factory);

                return provider =>
                {
                    var args = argsFactories
                        .Select(dependencyFactory => dependencyFactory(provider))
                        .ToArray();

                    return objectFactory(provider, args);
                };
            }
        }
    }
}