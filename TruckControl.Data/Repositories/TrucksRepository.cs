using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckControl.Business.Trucks.Domain;
using TruckControl.Business.Trucks.Dto;
using TruckControl.Business.Trucks.Repositories;

namespace TruckControl.Data.Repositories
{
    public class TrucksRepository : ITrucksRepository
    {
        public Task DeleteTruckByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TruckDto>> GetAllTrucksAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TruckDto> GetTruckByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<long> InsertTruckAsync(Truck truck)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTruckAsync(Truck truck)
        {
            throw new NotImplementedException();
        }
    }
}
