using System.Threading.Tasks;
using Data.Models;

namespace Services.Services
{
    public interface ILocationService
    {
        Task<OperationResult<LocationModel>> GetLocationAsync(AddressModel address);
    }
}
