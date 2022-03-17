using System.Collections.Generic;
using System.Threading.Tasks;
using TruckControl.Business.Trucks.Commands;
using TruckControl.Business.Trucks.Result;

namespace TruckControl.Business.Handlers.Interfaces
{
    public interface ITrucksHandler
    {
        Task<List<TruckResult>> GetAllTrucksAsync();
        Task<TruckResult> GetTruckByIdAsync(GetTruckByIdCommand command);
    }
}
