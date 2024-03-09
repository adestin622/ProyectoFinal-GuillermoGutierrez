using Microsoft.AspNetCore.Mvc;
using SGestionBussiness;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NombreController : Controller
    {
        [HttpGet]
        public string GetNombre()
        {
            return "Guillermo Gutierrez";
        }
    }
}
