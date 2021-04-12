namespace DependencyBuilder
{
    public static class ServiceDescriptorBuilderExtensions
    {
        public static ServiceDescriptorBuilder Override<TService, TImplementation>(this ServiceDescriptorBuilder serviceDescriptorBuilder)
        where TImplementation: TService =>
            serviceDescriptorBuilder.Override(typeof(TService), typeof(TImplementation));

        public static ServiceDescriptorBuilder Decorate<TDecorator>(
            this ServiceDescriptorBuilder serviceDescriptorBuilder) =>
            serviceDescriptorBuilder.Decorate(typeof(TDecorator));
    }
}