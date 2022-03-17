using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckControl.Business.Handlers.Interfaces;
using TruckControl.Business.Shared;
using TruckControl.Business.Shared.Validator;
using TruckControl.Business.Trucks.Commands;
using TruckControl.Business.Trucks.Domain;
using TruckControl.Business.Trucks.Repositories;
using TruckControl.Business.Trucks.Result;

namespace TruckControl.Business.Handlers
{
    public class TrucksHandler : Notifiable, ITrucksHandler
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

        public async Task UpdateTruckAsync(UpdateTruckCommand command)
        {
            var truck = Truck.Fabric.CreateTruckForUpdate(command.Id, command.Model, command.ManufacturingYear, command.ModelYear);

            if (!truck.Valid)
            {
                AddNotifications(truck.Notifications);
                return;
            }

            await _trucksRepository.UpdateTruckAsync(truck);
        }

        public async Task DeleteTruckByIdAsync(DeleteTruckByIdCommand command)
        {
            await _trucksRepository.DeleteTruckByIdAsync(command.Id);
        }

        public async Task<long?> InsertTruckAsync(InsertTruckCommand command)
        {
            var truck = Truck.Fabric.CreateTruckForInsert(command.Model, command.ManufacturingYear, command.ModelYear);

            if (!truck.Valid)
            {
                AddNotifications(truck.Notifications);
                return null;
            }

            var id = await _trucksRepository.InsertTruckAsync(truck);

            return id;
        }
    }
}
