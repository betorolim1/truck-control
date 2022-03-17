using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TruckControl.Api.Controllers;
using TruckControl.Business.Handlers.Interfaces;
using TruckControl.Business.Shared;
using TruckControl.Business.Trucks.Result;
using Xunit;

namespace TruckControl.Test.Controllers
{
    public class TrucksControllerTest
    {
        private Mock<ITrucksHandler> _trucksHandler = new Mock<ITrucksHandler>();

        [Fact]
        public async Task Should_return_OKObject()
        {
            var resultList = new List<TruckResult>
            {
                new TruckResult
                {
                    ManufacturingYear = 2022,
                    Model = ModelEnum.FH,
                    ModelYear = 2023
                }
            };

            _trucksHandler.Setup(x => x.GetAllTrucksAsync()).ReturnsAsync(resultList);

            var controller = NewController();

            var result = await controller.GetAllTrucksAsync() as OkObjectResult;

            Assert.NotNull(result);

            VerifyAll();
        }

        private TrucksController NewController() => new TrucksController(_trucksHandler.Object);

        private void VerifyAll()
        {
            _trucksHandler.VerifyAll();
            _trucksHandler.VerifyNoOtherCalls();
        }
    }
}
