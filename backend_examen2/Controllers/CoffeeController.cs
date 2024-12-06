using Microsoft.AspNetCore.Mvc;
using backend_examen2.Application;
using backend_examen2.Models;

namespace backend_examen2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeController : ControllerBase
    {
        private readonly CoffeeQuery _coffeeService;

        public CoffeeController(CoffeeQuery coffeeService)
        {
            _coffeeService = coffeeService;
        }

        [HttpGet]
        public ActionResult<List<Coffee>> GetCoffees()
        {
            var coffees = _coffeeService.GetCoffees();
            return Ok(coffees);
        }
    }
}