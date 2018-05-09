using System.Threading.Tasks;

namespace Gatherer.Infrastructure.Services
{
    public interface IDataInitializer
    {
        Task SeedAsync();
    }
}