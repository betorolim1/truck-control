using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckControl.Business.Trucks.Dto;
using TruckControl.Business.Trucks.Repositories;

namespace TruckControl.Data.Repositories
{
    public class TrucksRepository : ITrucksRepository
    {
        public async Task<List<TruckDto>> GetAllTrucksAsync()
        {
            throw new NotImplementedException();
        }
    }
}
