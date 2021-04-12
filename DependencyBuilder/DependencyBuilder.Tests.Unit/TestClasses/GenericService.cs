using System.Threading.Tasks;

namespace DependencyBuilder.Tests.Unit.TestClasses
{
    public class GenericService : IGenericService<Request, Response>
    {
        public Task<Response> Invoke(Request missing_name)
        {
            throw new System.NotImplementedException();
        }
    }
}