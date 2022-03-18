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
                            Id = dto.Id,
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
                Id = dto.Id,
                ManufacturingYear = dto.ManufacturingYear,
                Model = (ModelEnum)dto.Model,
                ModelYear = dto.ModelYear
            };

            return result;
        }

        public async Task UpdateTruckAsync(UpdateTruckCommand command)
        {
            var newTruck = TruckDomain.Fabric.CreateTruckForUpdate(command.Id, command.Model, command.ManufacturingYear, command.ModelYear);

            if (!newTruck.Valid)
            {
                AddNotifications(newTruck.Notifications);
                return;
            }

            var oldTruck = await _trucksRepository.GetTruckByIdAsync(newTruck.Id);

            if (oldTruck is null)
            {
                AddNotification("Truck does not exist");
                return;
            }

            await _trucksRepository.UpdateTruckAsync(oldTruck, newTruck);
        }

        public async Task DeleteTruckByIdAsync(DeleteTruckByIdCommand command)
        {
            var truck = await _trucksRepository.GetTruckByIdAsync(command.Id);

            if (truck is null)
                return;

            await _trucksRepository.DeleteTruckByIdAsync(truck);
        }

        public async Task<long?> InsertTruckAsync(InsertTruckCommand command)
        {
            var truck = TruckDomain.Fabric.CreateTruckForInsert(command.Model, command.ManufacturingYear, command.ModelYear);

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
