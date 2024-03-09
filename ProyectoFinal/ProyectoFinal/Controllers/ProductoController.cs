using Microsoft.AspNetCore.Mvc;
using SGestionBussiness;
using SGestionEntities;
using System.Net;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        [HttpGet("{idUsuario}")]
        public ActionResult<List<Producto>> TraerProductos(int idUsuario)
        {
            if (idUsuario < 0)
            {
                return base.BadRequest(new { message = "Ingrese un IdUsuario valido", status = HttpStatusCode.BadRequest });
            }
            else
            {
                try
                {
                    ProductoBussiness producSearch = new();
                    return producSearch.BuscarProducto(idUsuario);
                }
                catch (Exception ex) { return base.Conflict(new { message = $"Error al obtener los productos asociados al IdUsuario:{idUsuario}\nError:{ex.Message}", status = HttpStatusCode.Conflict}); }
            }
        }

        [HttpPost]
        public IActionResult CrearProducto([FromBody] Producto producto)
        {
            try
            {
                ProductoBussiness newProduc = new();
                if (newProduc.AgregarProducto(producto)) { return Ok(); }
                else { return Conflict("Error en la carga"); }
            }
            catch (Exception ex) { return base.Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict }); }
        }

        [HttpPut]
        public IActionResult ModificarProducto([FromBody] Producto producto)
        {
            try
            {
                ProductoBussiness newProduc = new();
                if (newProduc.ModificarProducto(producto)) { return Ok(new {message="Producto actualizado", nuevoProducto = producto, status = HttpStatusCode.OK }); }
                else { return Conflict("Error al actualizar el producto"); }
            }
            catch (Exception ex) { return base.Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict}); }
        }

        [HttpDelete("{idProducto}")]
        public IActionResult EliminarProducto(int idProducto)
        {
            if (idProducto < 0)
            {
                return base.BadRequest(new { message = "Ingrese un IdProducto valido", status = HttpStatusCode.BadRequest });
            }
            else
            {
                try
                {
                    ProductoBussiness newProduc = new();
                    if (newProduc.EliminarProducto(idProducto)) { return Ok(); }
                    else { return Conflict("Error al eliminar el producto"); }
                }
                catch (Exception ex) { return base.Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict }); }
            }
        }
    }
}
