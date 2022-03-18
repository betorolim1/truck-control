using System.Collections.Generic;
using System.Threading.Tasks;
using TruckControl.Business.Trucks.Domain;
using TruckControl.Model.EFModel;

namespace TruckControl.Business.Trucks.Repositories
{
    public interface ITrucksRepository
    {
        Task<List<Truck>> GetAllTrucksAsync();
        Task<Truck> GetTruckByIdAsync(long id);
        Task UpdateTruckAsync(Truck oldTruck, TruckDomain newTruck);
        Task DeleteTruckByIdAsync(Truck truck);
        Task<long> InsertTruckAsync(TruckDomain truck);
    }
}
