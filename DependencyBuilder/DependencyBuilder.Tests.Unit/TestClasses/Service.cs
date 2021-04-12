namespace DependencyBuilder.Tests.Unit.TestClasses
{
    public class Service : IService
    {
        public IRepository<Entity> Repo { get; }

        public Service(IRepository<Entity> repo)
        {
            Repo = repo;
        }
        
        public void Invoke()
        {
            throw new System.NotImplementedException();
        }
    }

    public class ServiceDecorator : IService
    {
        public IService Service { get; }

        public ServiceDecorator(IService service)
        {
            Service = service;
        }

        public void Invoke() => Service.Invoke();
    }
}