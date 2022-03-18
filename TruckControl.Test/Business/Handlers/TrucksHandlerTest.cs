using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckControl.Business.Handlers;
using TruckControl.Business.Shared;
using TruckControl.Business.Trucks.Commands;
using TruckControl.Business.Trucks.Domain;
using TruckControl.Business.Trucks.Repositories;
using TruckControl.Model.EFModel;
using Xunit;

namespace TruckControl.Test.Business.Handlers
{
    public class TrucksHandlerTest
    {
        private Mock<ITrucksRepository> _trucksRepositoryMock = new Mock<ITrucksRepository>();


        // GetAllTrucksAsync


        [Fact]
        public async Task GetAllTrucksAsync_Must_return_all_trucks()
        {
            var dto = new List<Truck>
            {
                new Truck
                {
                    Id = 12,
                    ManufacturingYear = 2022,
                    Model = 1,
                    ModelYear = 2023
                },
                new Truck
                {
                    Id  = 14,
                    ManufacturingYear = 2023,
                    Model = 0,
                    ModelYear = 2024
                }
            };

            _trucksRepositoryMock.Setup(x => x.GetAllTrucksAsync()).ReturnsAsync(dto);

            var handler = NewHandler();

            var result = await handler.GetAllTrucksAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            Assert.Equal(12, result[0].Id);
            Assert.Equal(2022, result[0].ManufacturingYear);
            Assert.Equal(ModelEnum.FM, result[0].Model);
            Assert.Equal(2023, result[0].ModelYear);

            VerifyAll();
        }


        // GetTruckByIdAsync


        [Fact]
        public async Task GetTruckByIdAsync_Must_return_truck_by_id()
        {
            var command = new GetTruckByIdCommand
            {
                Id = 12
            };

            var dto = new Truck
            {
                Id = 12,
                ManufacturingYear = 2022,
                Model = 1,
                ModelYear = 2023
            };

            _trucksRepositoryMock.Setup(x => x.GetTruckByIdAsync(12)).ReturnsAsync(dto);

            var handler = NewHandler();

            var result = await handler.GetTruckByIdAsync(command);

            Assert.NotNull(result);

            Assert.Equal(12, result.Id);
            Assert.Equal(2022, result.ManufacturingYear);
            Assert.Equal(ModelEnum.FM, result.Model);
            Assert.Equal(2023, result.ModelYear);

            VerifyAll();
        }

        [Fact]
        public async Task GetTruckByIdAsync_Must_return_null()
        {
            var command = new GetTruckByIdCommand
            {
                Id = 12
            };

            _trucksRepositoryMock.Setup(x => x.GetTruckByIdAsync(12));

            var handler = NewHandler();

            var result = await handler.GetTruckByIdAsync(command);

            Assert.Null(result);

            VerifyAll();
        }


        // UpdateTruckAsync


        [Fact]
        public async Task UpdateTruckAsync_Must_update()
        {
            var command = new UpdateTruckCommand
            {
                Id = 12,
                ManufacturingYear = 2000,
                Model = 1,
                ModelYear = 2001
            };

            var oldTruck = new Truck
            {
                Id = 11,
                ManufacturingYear = 1999,
                Model = 0,
                ModelYear = 2000
            };

            _trucksRepositoryMock.Setup(x => x.GetTruckByIdAsync(12)).ReturnsAsync(oldTruck);

            _trucksRepositoryMock.Setup(x => x.UpdateTruckAsync(oldTruck, It.IsAny<TruckDomain>()));

            var handler = NewHandler();

            await handler.UpdateTruckAsync(command);

            Assert.True(handler.Valid);

            VerifyAll();
        }

        [Fact]
        public async Task UpdateTruckAsync_Must_notify_if_truck_is_not_valid()
        {
            var command = new UpdateTruckCommand
            {
                Id = 0,
                ManufacturingYear = 2000,
                Model = 1,
                ModelYear = 2001
            };

            var handler = NewHandler();

            await handler.UpdateTruckAsync(command);

            Assert.False(handler.Valid);
            Assert.Contains(handler.Notifications, nf => nf == "Id must be greater than zero");

            VerifyAll();
        }
        
        [Fact]
        public async Task UpdateTruckAsync_Must_notify_if_truck_does_not_exist()
        {
            var command = new UpdateTruckCommand
            {
                Id = 12,
                ManufacturingYear = 2000,
                Model = 1,
                ModelYear = 2001
            };

            _trucksRepositoryMock.Setup(x => x.GetTruckByIdAsync(12));

            var handler = NewHandler();

            await handler.UpdateTruckAsync(command);

            Assert.False(handler.Valid);
            Assert.Contains(handler.Notifications, nf => nf == "Truck does not exist");

            VerifyAll();
        }


        // DeleteTruckByIdAsync


        [Fact]
        public async Task DeleteTruckByIdAsync_Must_delete()
        {
            var command = new DeleteTruckByIdCommand
            {
                Id = 12,
            };

            var oldTruck = new Truck
            {
                Id = 11,
                ManufacturingYear = 1999,
                Model = 0,
                ModelYear = 2000
            };

            _trucksRepositoryMock.Setup(x => x.GetTruckByIdAsync(12)).ReturnsAsync(oldTruck);

            _trucksRepositoryMock.Setup(x => x.DeleteTruckByIdAsync(12));

            var handler = NewHandler();

            await handler.DeleteTruckByIdAsync(command);

            VerifyAll();
        }

        [Fact]
        public async Task DeleteTruckByIdAsync_Must_not_notify_if_truck_does_not_exist()
        {
            var command = new DeleteTruckByIdCommand
            {
                Id = 12,
            };

            _trucksRepositoryMock.Setup(x => x.GetTruckByIdAsync(12));

            var handler = NewHandler();

            await handler.DeleteTruckByIdAsync(command);

            VerifyAll();
        }


        // InsertTruckAsync


        [Fact]
        public async Task InsertTruckAsync_Must_update()
        {
            var command = new InsertTruckCommand
            {
                ManufacturingYear = 2000,
                Model = 1,
                ModelYear = 2001
            };

            _trucksRepositoryMock.Setup(x => x.InsertTruckAsync(It.IsAny<TruckDomain>()));

            var handler = NewHandler();

            await handler.InsertTruckAsync(command);

            Assert.True(handler.Valid);

            VerifyAll();
        }

        [Fact]
        public async Task InsertTruckAsync_Must_notify_if_truck_if_not_valid()
        {
            var command = new InsertTruckCommand
            {
                ManufacturingYear = 2000,
                Model = 99,
                ModelYear = 2001
            };

            var handler = NewHandler();

            await handler.InsertTruckAsync(command);

            Assert.False(handler.Valid);
            Assert.Contains(handler.Notifications, nf => nf == "Model is invalid");

            VerifyAll();
        }

        private TrucksHandler NewHandler() => new TrucksHandler(_trucksRepositoryMock.Object);

        private void VerifyAll()
        {
            _trucksRepositoryMock.VerifyAll();
            _trucksRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
