using Microsoft.AspNetCore.Mvc;
using SGestionBussiness;
using SGestionEntities;
using System.Net;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoVendidoController : Controller
    {
        [HttpGet("{idUsuario}")]
        public ActionResult<List<ProductoVendido>> TraerProductosVendidos(int idUsuario) 
        {
            try
            {
                if (idUsuario >= 0)
                {
                    ProductoVendidoBussiness producVendidos = new();
                    return base.Accepted(producVendidos.TraerProductosVendidos(idUsuario));
                }
                else
                {
                    base.BadRequest(new { mensaje = "El Id de Usuario debe ser positivo" , status = HttpStatusCode.BadRequest});
                    return null;
                }
                
            }catch (Exception ex) 
            {
                base.Conflict(new { mensaje = $"Error al traer los productos vendidos\n{ex}", status = HttpStatusCode.Conflict});
                return null;
            }
        }
    }
}
