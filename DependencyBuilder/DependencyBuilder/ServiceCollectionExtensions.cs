using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyBuilder
{
    public static class ServiceCollectionExtensions
    {
        public static void Add(
            this IServiceCollection services, 
            Type serviceType,
            Type implementationType,
            ServiceLifetime serviceLifetime,
            Action<ServiceDescriptorBuilder> options)
        {
            var builder = new ServiceDescriptorBuilder(serviceType, implementationType)
                .Lifetime(serviceLifetime);
            options(builder);
            AddUnregisteredServices(services, serviceLifetime, builder);

            services.Add(builder.Build());
        }

        private static void AddUnregisteredServices(IServiceCollection services, ServiceLifetime serviceLifetime,
            ServiceDescriptorBuilder builder)
        {
            var serviceTypes = services.Select(_ => _.ServiceType).ToHashSet();

            foreach (var type in builder.GetOverriddenTypes().Where(_ => !serviceTypes.Contains(_)))
                services.Add(new ServiceDescriptor(type, type, serviceLifetime));
        }

        public static void Add<TService, TImplementation>(
            this IServiceCollection services,
            ServiceLifetime serviceLifetime,
            Action<ServiceDescriptorBuilder> options) =>
            services.Add(typeof(TService), typeof(TImplementation), serviceLifetime, options);
    }
}