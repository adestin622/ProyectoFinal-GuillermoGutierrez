using System;
using System.Collections.Generic;

namespace SGestionData.EntityContext
{
    public partial class UsuarioContext
    {
        public UsuarioContext()
        {
            Productos = new HashSet<ProductoContext>();
            Venta = new HashSet<VentumContext>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string Mail { get; set; } = null!;

        public virtual ICollection<ProductoContext> Productos { get; set; }
        public virtual ICollection<VentumContext> Venta { get; set; }
    }
}
