using Microsoft.AspNetCore.Mvc;
using SGestionBussiness;
using SGestionEntities;
using System.Net;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        [HttpPut]
        public IActionResult ModificarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                UsuarioBussiness newUser = new();
                if (newUser.ModificarUsuario(usuario)) { return Ok(); }
                else { return Conflict("Error al actualizar el usuario"); }
            }
            catch (Exception ex) { return Conflict(ex); }
        }

        [HttpGet("{usuario}/{password}")]
        public ActionResult<Usuario> InicioDeSesion(string usuario, string password)
        {
            try
            {
                UsuarioBussiness user = new();
                Usuario login = user.InicioSesion(usuario, password);
                if (login != null)
                {
                    return base.Accepted(login);
                }
                else { return base.BadRequest(new { mensaje = "User or Pass incorrect", status = HttpStatusCode.BadRequest }); }
                
            }
            catch (Exception ex) { return base.Conflict(new { mensaje = $"Error en el inicio de sesion\n{ex}", status = HttpStatusCode.Conflict }); }
        }

        [HttpPost]
        public IActionResult CrearUsuario([FromBody] Usuario user)
        {
            try
            {
                UsuarioBussiness userNew = new();
                return base.Accepted(userNew.CrearUsuario(user));
            }catch (Exception ex) { return base.BadRequest(new { mensaje = $"Error al crear el usuario\n {ex}", status = HttpStatusCode.BadRequest}); }
        }

        [HttpGet("{nombreUsuario}")]
        public ActionResult<Usuario> TraerUsuario(string nombreUsuario)
        {
            try
            {
                UsuarioBussiness user = new();
                Usuario usuario = user.TraerUsuario(nombreUsuario);
                if (usuario is not null)
                {
                    return base.Ok(usuario);
                }
                else { return base.NotFound(new { mensaje = "Usuario no encontrado", status = HttpStatusCode.NotFound }); }
                
            }catch(Exception ex) { return base.BadRequest(new { mensaje = $"Error al traer el usuario\n {ex}", status = HttpStatusCode.BadGateway }); }
        }

        [HttpDelete("{idUsuario}")]
        public IActionResult EliminarUsuario(int idUsuario)
        {
            try
            {
                UsuarioBussiness user = new();
                if (idUsuario >= 0 && user.EliminarUsuario(idUsuario))
                {
                    return base.Ok(new { mensaje = $"Usuario N°{idUsuario} eliminado", status = HttpStatusCode.OK });
                }
                else { return base.BadRequest(new { mensaje = "No se pudo eliminar el usuario, ingrese Id nuevamente", status = HttpStatusCode.BadRequest }); }
            }catch (Exception ex) { return base.Conflict(new { mensaje = $"Error al eliminar el usuario\n{ex}", status = HttpStatusCode.Conflict}); }
        }
    }
}
