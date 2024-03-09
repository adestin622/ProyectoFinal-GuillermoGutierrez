using Microsoft.AspNetCore.Mvc;
using System.Net;
using SGestionBussiness;
using SGestionEntities;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : Controller
    {
        [HttpPost("{idUsuario}")]
        public IActionResult CargarVenta([FromBody] List<Producto> productos, int idUsuario)
        {
            try
            {
                if (productos.Count > 0)
                {
                    VentaBussiness newVenta = new();
                    if (newVenta.AgregarVenta(productos, idUsuario))
                    {
                        return base.Ok(new { mensaje = "Carga Exitosa", status = HttpStatusCode.OK });
                    }
                    else
                    {
                        return base.Conflict(new { mensaje = "Error en la carga", status = HttpStatusCode.Conflict });
                    }
                }
                return base.BadRequest(new { mensaje = "No se recibieron productos para la venta", status = HttpStatusCode.BadRequest });
            }
            catch (Exception ex) { return base.Conflict(new { mensaje = $"Error al cargar la venta:\n {ex}" }); }
        }

        [HttpGet("{idUsuario}")]
        public ActionResult<List<Venta>> TraerVentas(int idUsuario)
        {
            try
            {
                if (idUsuario >= 0)
                {
                    VentaBussiness newVenta = new();
                    return newVenta.ListarVentas(idUsuario);
                }
                else
                {
                    return base.BadRequest(new { mensaje = "Ingrese un Id de Usuario valido", status = HttpStatusCode.BadRequest });
                }
            }
            catch (Exception ex) { return base.Conflict(new { mensaje = $"Error al listar las venta:\n {ex}" }); }
        }

        [HttpDelete("{idVenta}")]
        public IActionResult DeleteVenta(int idVenta)
        {
            try
            {
                VentaBussiness venta = new();
                if (idVenta >= 0 && venta.EliminarVenta(idVenta))
                {
                    return base.Ok(new { mensaje = $"Venta N°{idVenta} eliminada", status = HttpStatusCode.OK });
                }
                else { return base.BadRequest(new { mensaje = "No se pudo eliminar la venta, ingrese Id nuevamente", status = HttpStatusCode.BadRequest }); }
            }
            catch (Exception ex) { return base.Conflict(new { mensaje = $"Error al eliminar la venta\n{ex}", status = HttpStatusCode.Conflict }); }
        }
    }
}
