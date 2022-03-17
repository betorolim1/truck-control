using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TruckControl.Business.Handlers.Interfaces;
using TruckControl.Business.Trucks.Commands;

namespace TruckControl.Api.Controllers
{
    [Route(Routes.Trucks)]
    public class TrucksController : Controller
    {
        private ITrucksHandler _trucksHandler { get; set; }

        public TrucksController(ITrucksHandler trucksHandler)
        {
            _trucksHandler = trucksHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrucksAsync()
        {
            var result = await _trucksHandler.GetAllTrucksAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTruckByIdAsync(long id)
        {
            var command = new GetTruckByIdCommand
            {
                Id = id
            };

            var result = await _trucksHandler.GetTruckByIdAsync(command);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTruckAsync([FromBody] UpdateTruckCommand command)
        {
            if (command is null)
                return BadRequest("Command cannot be null");

            await _trucksHandler.UpdateTruckAsync(command);

            if (!_trucksHandler.Valid)
                return BadRequest(_trucksHandler.Notifications);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruckByIdAsync(long id)
        {
            var command = new DeleteTruckByIdCommand
            {
                Id = id
            };

            await _trucksHandler.DeleteTruckByIdAsync(command);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> InsertTruckAsync([FromBody]InsertTruckCommand command)
        {
            if (command is null)
                return BadRequest("Command cannot be null");

            var result = await _trucksHandler.InsertTruckAsync(command);

            if (!_trucksHandler.Valid)
                return BadRequest(_trucksHandler.Notifications);

            return Ok(result);
        }
    }
}
