using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TruckControl.Business.Handlers.Interfaces;

namespace TruckControl.Api.Controllers
{
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
    }
}
