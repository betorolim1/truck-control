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
        public async Task GetAllTrucksAsync_Must_return_OKObject()
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
        public async Task GetTruckByIdAsync_Must_return_OKObject()
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


        // UpdateTruckAsync


        [Fact]
        public async Task UpdateTruckAsync_Must_return_NoContent()
        {
            var command = new UpdateTruckCommand
            {
                Id = 12,
                ManufacturingYear = 2022,
                Model = 1,
                ModelYear = 2023
            };

            _trucksHandlerMock.Setup(x => x.UpdateTruckAsync(command));
            _trucksHandlerMock.Setup(x => x.Valid).Returns(true);

            var controller = NewController();

            var result = await controller.UpdateTruckAsync(command) as NoContentResult;

            Assert.NotNull(result);

            VerifyAll();
        }

        [Fact]
        public async Task UpdateTruckAsync_Must_notify_if_command_is_null()
        {
            var controller = NewController();

            var result = await controller.UpdateTruckAsync(null) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal("Command cannot be null", result.Value);

            VerifyAll();
        }

        [Fact]
        public async Task UpdateTruckAsync_Must_notify_if_handler_is_not_valid()
        {
            var notify = new List<string> { "NotifyTest" };

            var command = new UpdateTruckCommand
            {
                Id = 0,
                ManufacturingYear = 2022,
                Model = 1,
                ModelYear = 2023
            };

            _trucksHandlerMock.Setup(x => x.UpdateTruckAsync(command));
            _trucksHandlerMock.Setup(x => x.Valid).Returns(false);
            _trucksHandlerMock.Setup(x => x.Notifications).Returns(notify);

            var controller = NewController();

            var result = await controller.UpdateTruckAsync(command) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal(notify, result.Value);

            VerifyAll();
        }


        //DeleteTruckByIdAsync


        [Fact]
        public async Task DeleteTruckByIdAsync_Must_return_NoContent()
        {
            _trucksHandlerMock.Setup(x => x.DeleteTruckByIdAsync(It.IsAny<DeleteTruckByIdCommand>()));

            var controller = NewController();

            var result = await controller.DeleteTruckByIdAsync(12) as NoContentResult;

            Assert.NotNull(result);

            VerifyAll();
        }


        // InsertTruckAsync


        [Fact]
        public async Task InsertTruckAsync_Must_return_NoContent()
        {
            var command = new InsertTruckCommand
            {
                ManufacturingYear = 2022,
                Model = 1,
                ModelYear = 2023
            };

            _trucksHandlerMock.Setup(x => x.InsertTruckAsync(command)).ReturnsAsync(1234);
            _trucksHandlerMock.Setup(x => x.Valid).Returns(true);

            var controller = NewController();

            var result = await controller.InsertTruckAsync(command) as OkObjectResult;

            Assert.NotNull(result);

            VerifyAll();
        }

        [Fact]
        public async Task InsertTruckAsync_Must_notify_if_command_is_null()
        {
            var controller = NewController();

            var result = await controller.InsertTruckAsync(null) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal("Command cannot be null", result.Value);

            VerifyAll();
        }

        [Fact]
        public async Task InsertTruckAsync_Must_notify_if_handler_is_not_valid()
        {
            var notify = new List<string> { "NotifyTest" };

            var command = new InsertTruckCommand
            {
                ManufacturingYear = 2022,
                Model = 99,
                ModelYear = 2023
            };

            _trucksHandlerMock.Setup(x => x.InsertTruckAsync(command));
            _trucksHandlerMock.Setup(x => x.Valid).Returns(false);
            _trucksHandlerMock.Setup(x => x.Notifications).Returns(notify);

            var controller = NewController();

            var result = await controller.InsertTruckAsync(command) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal(notify, result.Value);

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
