using System.Collections.Generic;
using System.Threading.Tasks;
using TruckControl.Business.Trucks.Domain;
using TruckControl.Business.Trucks.Dto;

namespace TruckControl.Business.Trucks.Repositories
{
    public interface ITrucksRepository
    {
        Task<List<TruckDto>> GetAllTrucksAsync();
        Task<TruckDto> GetTruckByIdAsync(long id);
        Task UpdateTruckAsync(Truck truck);
    }
}
