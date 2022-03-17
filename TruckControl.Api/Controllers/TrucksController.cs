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
    }
}
