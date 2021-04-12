using System;
using DependencyBuilder.Tests.Unit.TestClasses;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace DependencyBuilder.Tests.Unit
{
    public class ServiceCollectionExtensionsTests
    {
        [Theory]
        [AutoArguments]
        public void Should_override_using_existing_service(
            ServiceCollection serviceCollection,
            ServiceLifetime serviceLifetime)
        {
            serviceCollection.AddTransient<IRepository<Entity>, Repository<Entity>>();
            serviceCollection.AddTransient<AlternativeRepository<Entity>>();

            serviceCollection
                .Add<IService, Service>(serviceLifetime, opt => opt
                    .Override<IRepository<Entity>, AlternativeRepository<Entity>>());

            var provider = serviceCollection.BuildServiceProvider();
            var service = provider.GetRequiredService<IService>();

            service.As<Service>().Repo.Should().BeOfType<AlternativeRepository<Entity>>();
        }
        
        [Theory]
        [AutoArguments]
        public void Should_override_using_new_service(
            ServiceCollection serviceCollection,
            ServiceLifetime serviceLifetime)
        {
            serviceCollection.AddTransient<IRepository<Entity>, Repository<Entity>>();

            serviceCollection
                .Add<IService, Service>(serviceLifetime, opt => opt
                    .Override<IRepository<Entity>, AlternativeRepository<Entity>>());

            var provider = serviceCollection.BuildServiceProvider();
            var service = provider.GetRequiredService<IService>();

            service.As<Service>().Repo.Should().BeOfType<AlternativeRepository<Entity>>();
        }
        
        [Theory]
        [AutoArguments]
        public void Should_decorate_using_new_service(
            ServiceCollection serviceCollection,
            ServiceLifetime serviceLifetime)
        {

            serviceCollection
                .Add<IService, Service>(serviceLifetime, opt => opt
                    .Decorate<ServiceDecorator>());

            var provider = serviceCollection.BuildServiceProvider();
            var service = provider.GetRequiredService<IService>();

            service.As<Service>().Repo.Should().BeOfType<AlternativeRepository<Entity>>();
        }
        
        
        //
        // [Theory]
        // [AutoArguments]
        // public void Should_allow_registering_using_options_action(
        //     ServiceCollection serviceCollection)
        // {
        //     serviceCollection.Add(descriptor, opt => { });
        // }
    }
}