using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGestionEntities
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string NombreUsuario { get; set; } = null!;

        public string Contrasena { get; set; } = null!;

        public string Mail { get; set; } = null!;
    }
}
