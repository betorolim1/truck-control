using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckControl.Api.Controllers;
using TruckControl.Business.Handlers.Interfaces;
using TruckControl.Business.Shared;
using TruckControl.Business.Trucks.Commands;
using TruckControl.Business.Trucks.Result;
using Xunit;

namespace TruckControl.Test.Controllers
{
    public class TrucksControllerTest
    {
        private Mock<ITrucksHandler> _trucksHandlerMock = new Mock<ITrucksHandler>();


        //GetAllTrucksAsync


        [Fact]
        public async Task GetAllTrucksAsync_Should_return_OKObject()
        {
            var resultList = new List<TruckResult>
            {
                new TruckResult
                {
                    Id = 12,
                    ManufacturingYear = 2022,
                    Model = ModelEnum.FH,
                    ModelYear = 2023
                }
            };

            _trucksHandlerMock.Setup(x => x.GetAllTrucksAsync()).ReturnsAsync(resultList);

            var controller = NewController();

            var result = await controller.GetAllTrucksAsync() as OkObjectResult;

            Assert.NotNull(result);

            VerifyAll();
        }


        // GetTruckByIdAsync


        [Fact]
        public async Task GetTruckByIdAsync_Should_return_OKObject()
        {
            var resultHandler = new TruckResult
            {
                Id = 12,
                ManufacturingYear = 2022,
                Model = ModelEnum.FH,
                ModelYear = 2023
            };

            _trucksHandlerMock.Setup(x => x.GetTruckByIdAsync(It.IsAny<GetTruckByIdCommand>())).ReturnsAsync(resultHandler);

            var controller = NewController();

            var result = await controller.GetTruckByIdAsync(12) as OkObjectResult;

            Assert.NotNull(result);

            VerifyAll();
        }

        private TrucksController NewController() => new TrucksController(_trucksHandlerMock.Object);

        private void VerifyAll()
        {
            _trucksHandlerMock.VerifyAll();
            _trucksHandlerMock.VerifyNoOtherCalls();
        }
    }
}
