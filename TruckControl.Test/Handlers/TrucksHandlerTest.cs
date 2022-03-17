using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckControl.Api.Controllers;
using TruckControl.Business.Handlers;
using TruckControl.Business.Handlers.Interfaces;
using TruckControl.Business.Shared;
using TruckControl.Business.Trucks.Dto;
using TruckControl.Business.Trucks.Repositories;
using TruckControl.Business.Trucks.Result;
using Xunit;

namespace TruckControl.Test.Handlers
{
    public class TrucksHandlerTest
    {
        private Mock<ITrucksRepository> _trucksRepository = new Mock<ITrucksRepository>();

        [Fact]
        public async Task Should_return_all_trucks()
        {
            var resultList = new List<TruckDto>
            {
                new TruckDto
                {
                    ManufacturingYear = 2022,
                    Model = 1,
                    ModelYear = 2023
                },
                new TruckDto
                {
                    ManufacturingYear = 2023,
                    Model = 0,
                    ModelYear = 2024
                }
            };

            _trucksRepository.Setup(x => x.GetAllTrucksAsync()).ReturnsAsync(resultList);

            var handler = NewHandler();

            var result = await handler.GetAllTrucksAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            Assert.Equal(2022, result[0].ManufacturingYear);
            Assert.Equal(ModelEnum.FM, result[0].Model);
            Assert.Equal(2023, result[0].ModelYear);

            VerifyAll();
        }

        private TrucksHandler NewHandler() => new TrucksHandler(_trucksRepository.Object);

        private void VerifyAll()
        {
            _trucksRepository.VerifyAll();
            _trucksRepository.VerifyNoOtherCalls();
        }
    }
}
