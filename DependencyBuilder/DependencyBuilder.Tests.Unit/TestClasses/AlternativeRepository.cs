using System.Threading.Tasks;

namespace DependencyBuilder.Tests.Unit.TestClasses
{
    public class AlternativeRepository<T> : IRepository<T>
    {
        public Task Create(T item)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> Get(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}