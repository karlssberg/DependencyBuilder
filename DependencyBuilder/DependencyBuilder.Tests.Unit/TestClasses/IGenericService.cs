using System.Threading.Tasks;

namespace DependencyBuilder.Tests.Unit.TestClasses
{
    public interface IGenericService<in TRequest, TResponse>
    {
        
        Task<TResponse> Invoke(TRequest request);
    }
}