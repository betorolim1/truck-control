using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckControl.Business.Handlers.Interfaces;
using TruckControl.Business.Shared;
using TruckControl.Business.Trucks.Commands;
using TruckControl.Business.Trucks.Repositories;
using TruckControl.Business.Trucks.Result;

namespace TruckControl.Business.Handlers
{
    public class TrucksHandler : ITrucksHandler
    {
        public ITrucksRepository _trucksRepository { get; set; }

        public TrucksHandler(ITrucksRepository trucksRepository)
        {
            _trucksRepository = trucksRepository;
        }

        public async Task<List<TruckResult>> GetAllTrucksAsync()
        {
            var dtoList = await _trucksRepository.GetAllTrucksAsync();

            var resultList = new List<TruckResult>();

            foreach (var dto in dtoList)
            {
                resultList.Add(
                        new TruckResult
                        {
                            ManufacturingYear = dto.ManufacturingYear,
                            Model = (ModelEnum)dto.Model,
                            ModelYear = dto.ModelYear
                        }
                    );
            }

            return resultList;
        }

        public async Task<TruckResult> GetTruckByIdAsync(GetTruckByIdCommand command)
        {
            var dto = await _trucksRepository.GetTruckByIdAsync(command.Id);

            if (dto is null)
                return null;

            var result = new TruckResult
            {
                ManufacturingYear = dto.ManufacturingYear,
                Model = (ModelEnum)dto.Model,
                ModelYear = dto.ModelYear
            };

            return result;
        }
    }
}
