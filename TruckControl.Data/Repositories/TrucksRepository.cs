using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckControl.Business.Trucks.Domain;
using TruckControl.Business.Trucks.Repositories;
using TruckControl.Data.Repositories.Base;
using TruckControl.Model.EFModel;

namespace TruckControl.Data.Repositories
{
    public class TrucksRepository : BaseRepository<Truck>, ITrucksRepository
    {
        public TrucksRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task DeleteTruckByIdAsync(Truck truck)
        {
            await RemoveAsync(truck);
        }

        public async Task<List<Truck>> GetAllTrucksAsync()
        {
            return await AppDbContext.Truck.ToListAsync();
        }

        public async Task<Truck> GetTruckByIdAsync(long id)
        {
            return await AppDbContext.Truck.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> InsertTruckAsync(TruckDomain truck)
        {
            var truckEf = new Truck
            {
                ManufacturingYear = truck.ManufacturingYear,
                Model = (int)truck.Model,
                ModelYear = truck.ModelYear
            };

            await AddAsync(truckEf);

            return truckEf.Id;
        }

        public async Task UpdateTruckAsync(Truck oldTruck, TruckDomain newTruck)
        {
            oldTruck.ManufacturingYear = newTruck.ManufacturingYear;
            oldTruck.Model = (int)newTruck.Model;
            oldTruck.ModelYear = newTruck.ModelYear;

            await UpdateAsync(oldTruck);
        }
    }
}
