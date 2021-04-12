using System.Threading.Tasks;

namespace DependencyBuilder.Tests.Unit.TestClasses
{
    public interface IRepository<T>
    {
        public Task Create(T item);
        public Task<T> Get(string id);
    }
}