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
                if (ex.Message.Contains("Dinero insuficiente para realizar la compra"))
                {
                    return BadRequest(new { message = ex.Message });
                }
                else if (ex.Message.Contains("Cantidad insuficiente de café"))
                {
                    return StatusCode(StatusCodes.Status409Conflict, new { message = ex.Message });
                }
                else if (ex.Message.Contains("No se puede proporcionar el vuelto exacto con las monedas disponibles"))
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new { message = ex.Message });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error interno del servidor" });
                }
            }
        }
    }
}