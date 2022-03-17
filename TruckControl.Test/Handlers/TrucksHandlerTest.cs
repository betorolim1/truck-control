using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckControl.Business.Handlers;
using TruckControl.Business.Shared;
using TruckControl.Business.Trucks.Commands;
using TruckControl.Business.Trucks.Dto;
using TruckControl.Business.Trucks.Repositories;
using Xunit;

namespace TruckControl.Test.Handlers
{
    public class TrucksHandlerTest
    {
        private Mock<ITrucksRepository> _trucksRepositoryMock = new Mock<ITrucksRepository>();


        // GetAllTrucksAsync


        [Fact]
        public async Task GetAllTrucksAsync_Should_return_all_trucks()
        {
            var dto = new List<TruckDto>
            {
                new TruckDto
                {
                    Id = 12,
                    ManufacturingYear = 2022,
                    Model = 1,
                    ModelYear = 2023
                },
                new TruckDto
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

            Assert.Equal(2022, result[0].ManufacturingYear);
            Assert.Equal(ModelEnum.FM, result[0].Model);
            Assert.Equal(2023, result[0].ModelYear);

            VerifyAll();
        }


        // GetTruckByIdAsync


        [Fact]
        public async Task GetTruckByIdAsync_Should_return_truck_by_id()
        {
            var command = new GetTruckByIdCommand
            {
                Id = 12
            };

            var dto = new TruckDto
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

            Assert.Equal(2022, result.ManufacturingYear);
            Assert.Equal(ModelEnum.FM, result.Model);
            Assert.Equal(2023, result.ModelYear);

            VerifyAll();
        }

        [Fact]
        public async Task GetTruckByIdAsync_Should_return_null()
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

        private TrucksHandler NewHandler() => new TrucksHandler(_trucksRepositoryMock.Object);

        private void VerifyAll()
        {
            _trucksRepositoryMock.VerifyAll();
            _trucksRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
