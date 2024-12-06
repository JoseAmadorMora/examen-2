using Microsoft.AspNetCore.Mvc;
using backend_examen2.Application;
using backend_examen2.Models;

namespace backend_examen2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly PurchaseCommand _purchaseService;

        public PurchaseController(PurchaseCommand purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost]
        public ActionResult MakePurchase([FromBody] PurchaseRequest request)
        {
            try
            {
                var change = _purchaseService.MakePurchase(request);
                return Ok(new { message = "Compra realizada con éxito", change });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}